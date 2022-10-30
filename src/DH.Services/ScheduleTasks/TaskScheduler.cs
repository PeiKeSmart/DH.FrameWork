using DH.Core;
using DH.Core.Configuration;
using DH.Core.Http;
using DH.Core.Infrastructure;
using DH.Entity;
using DH.Services.Localization;

using Microsoft.Extensions.DependencyInjection;

using NewLife.Log;

using XCode.Membership;

namespace DH.Services.ScheduleTasks
{
    /// <summary>
    /// 表示任务管理器
    /// </summary>
    public partial class TaskScheduler : ITaskScheduler
    {
        #region Fields

        protected static readonly List<TaskThread> _taskThreads = new();
        protected readonly AppSettings _appSettings;
        protected readonly IStoreContext _storeContext;

        #endregion

        #region Ctor

        public TaskScheduler(AppSettings appSettings,
            IHttpClientFactory httpClientFactory,
            IServiceScopeFactory serviceScopeFactory,
            IStoreContext storeContext)
        {
            _appSettings = appSettings;
            TaskThread.HttpClientFactory = httpClientFactory;
            TaskThread.ServiceScopeFactory = serviceScopeFactory;
            _storeContext = storeContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 初始化任务管理器
        /// </summary>
        public async Task InitializeAsync()
        {
            if (!DHSetting.Current.IsInstalled)
                return;

            if (_taskThreads.Any())
                return;

            // 初始化和启动计划任务
            var scheduleTasks = (ScheduleTask.GetAllTasks())
                .OrderBy(x => x.Seconds)
                .ToList();

            var store = _storeContext.GetCurrentStore();

            var scheduleTaskUrl = $"{store.Url.TrimEnd('/')}/{DHTaskDefaults.ScheduleTaskPath}";
            var timeout = _appSettings.Get<CommonConfig>().ScheduleTaskRunTimeout;

            foreach (var scheduleTask in scheduleTasks)
            {
                var taskThread = new TaskThread(scheduleTask, scheduleTaskUrl, timeout)
                {
                    Seconds = scheduleTask.Seconds
                };

                // 有时一个任务周期可以设置为几个小时（甚至几天），在这种情况下，它运行的概率非常小（应用程序可以重新启动），在开始中断的任务之前计算时间
                if (scheduleTask.LastStartUtc > DateTime.MinValue)
                {
                    // 上次启动后还剩秒数
                    var secondsLeft = (DateTime.UtcNow - scheduleTask.LastStartUtc).TotalSeconds;

                    if (secondsLeft >= scheduleTask.Seconds)
                        // 立即运行（立即）
                        taskThread.InitSeconds = 0;
                    else
                        // 计算开始时间并将其四舍五入
                        //(因此 "ensureRunOncePerPeriod"参数很好)
                        taskThread.InitSeconds = (int)(scheduleTask.Seconds - secondsLeft) + 1;
                }
                else if (scheduleTask.LastEnabledUtc > DateTime.MinValue)
                {
                    // 上次启用后剩余秒数
                    var secondsLeft = (DateTime.UtcNow - scheduleTask.LastEnabledUtc).TotalSeconds;

                    if (secondsLeft >= scheduleTask.Seconds)
                        // 立即运行（立即）
                        taskThread.InitSeconds = 0;
                    else
                        // 计算开始时间并将其四舍五入
                        //(因此 "ensureRunOncePerPeriod"参数很好)
                        taskThread.InitSeconds = (int)(scheduleTask.Seconds - secondsLeft) + 1;
                }
                else
                    // first start of a task
                    taskThread.InitSeconds = scheduleTask.Seconds;

                _taskThreads.Add(taskThread);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 启动任务计划程序
        /// </summary>
        public void StartScheduler()
        {
            foreach (var taskThread in _taskThreads)
                taskThread.InitTimer();
        }

        /// <summary>
        /// 停止任务计划程序
        /// </summary>
        public void StopScheduler()
        {
            foreach (var taskThread in _taskThreads)
                taskThread.Dispose();
        }

        #endregion

        #region Nested class

        /// <summary>
        /// 表示任务线程
        /// </summary>
        protected partial class TaskThread : IDisposable
        {
            #region Fields

            protected readonly string _scheduleTaskUrl;
            protected readonly ScheduleTask _scheduleTask;
            protected readonly int? _timeout;

            protected Timer _timer;
            protected bool _disposed;

            internal static IHttpClientFactory HttpClientFactory { get; set; }
            internal static IServiceScopeFactory ServiceScopeFactory { get; set; }

            #endregion

            #region Ctor

            public TaskThread(ScheduleTask task, string scheduleTaskUrl, int? timeout)
            {
                _scheduleTaskUrl = scheduleTaskUrl;
                _scheduleTask = task;
                _timeout = timeout;

                Seconds = 10 * 60;
            }

            #endregion

            #region Utilities

            private async Task RunAsync()
            {
                if (Seconds <= 0)
                    return;

                StartedUtc = DateTime.UtcNow;
                IsRunning = true;
                HttpClient client = null;

                try
                {
                    //create and configure client
                    client = HttpClientFactory.CreateClient(DHHttpDefaults.DefaultHttpClient);
                    if (_timeout.HasValue)
                        client.Timeout = TimeSpan.FromMilliseconds(_timeout.Value);

                    //send post data
                    var data = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("taskType", _scheduleTask.Type) });
                    await client.PostAsync(_scheduleTaskUrl, data);
                }
                catch (Exception ex)
                {
                    using var scope = ServiceScopeFactory.CreateScope();

                    // Resolve
                    var localizationService = EngineContext.Current.Resolve<ILocalizationService>(scope);
                    var storeContext = EngineContext.Current.Resolve<IStoreContext>(scope);

                    var message = ex.InnerException?.GetType() == typeof(TaskCanceledException) ? localizationService.GetResource("ScheduleTasks.TimeoutError") : ex.Message;
                    var store = storeContext.GetCurrentStore();

                    message = string.Format(localizationService.GetResource("ScheduleTasks.Error"), _scheduleTask.Name,
                        message, _scheduleTask.Type, store.Name, _scheduleTaskUrl);

                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();

                    // 获取当前客户
                    var currentCustomer = EngineContext.Current.Resolve<IWorkContext>().GetCurrentCustomer();

                    // 错误日志
                    XTrace.WriteException(ex);
                    LogProvider.Provider?.WriteLog("计划任务", "错误", false, message + " " + Environment.NewLine + ex.GetMessage(), currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
                }
                finally
                {
                    client?.Dispose();
                }

                IsRunning = false;
            }

            private void TimerHandler(object state)
            {
                try
                {
                    _timer.Change(-1, -1);

                    RunAsync().Wait();
                }
                catch
                {
                    // ignore
                }
                finally
                {
                    if (!_disposed && _timer != null)
                    {
                        if (RunOnlyOnce)
                            Dispose();
                        else
                            _timer.Change(Interval, Interval);
                    }
                }
            }

            #endregion

            #region Methods

            /// <summary>
            /// Disposes the instance
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            // Protected implementation of Dispose pattern.
            protected virtual void Dispose(bool disposing)
            {
                if (_disposed)
                    return;

                if (disposing)
                    lock (this)
                        _timer?.Dispose();

                _disposed = true;
            }

            /// <summary>
            /// Inits a timer
            /// </summary>
            public void InitTimer()
            {
                _timer ??= new Timer(TimerHandler, null, InitInterval, Interval);
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets or sets the interval in seconds at which to run the tasks
            /// </summary>
            public int Seconds { get; set; }

            /// <summary>
            /// Get or set the interval before timer first start 
            /// </summary>
            public int InitSeconds { get; set; }

            /// <summary>
            /// Get or sets a datetime when thread has been started
            /// </summary>
            public DateTime StartedUtc { get; private set; }

            /// <summary>
            /// Get or sets a value indicating whether thread is running
            /// </summary>
            public bool IsRunning { get; private set; }

            /// <summary>
            /// Gets the interval (in milliseconds) at which to run the task
            /// </summary>
            public int Interval
            {
                get
                {
                    //if somebody entered more than "2147483" seconds, then an exception could be thrown (exceeds int.MaxValue)
                    var interval = Seconds * 1000;
                    if (interval <= 0)
                        interval = int.MaxValue;
                    return interval;
                }
            }

            /// <summary>
            /// Gets the due time interval (in milliseconds) at which to begin start the task
            /// </summary>
            public int InitInterval
            {
                get
                {
                    //if somebody entered less than "0" seconds, then an exception could be thrown
                    var interval = InitSeconds * 1000;
                    if (interval <= 0)
                        interval = 0;
                    return interval;
                }
            }

            /// <summary>
            /// Gets or sets a value indicating whether the thread would be run only once (on application start)
            /// </summary>
            public bool RunOnlyOnce { get; set; }

            /// <summary>
            /// Gets a value indicating whether the timer is started
            /// </summary>
            public bool IsStarted => _timer != null;

            /// <summary>
            /// Gets a value indicating whether the timer is disposed
            /// </summary>
            public bool IsDisposed => _disposed;

            #endregion
        }

        #endregion
    }
}
