using DH.Core;
using DH.Core.Caching;
using DH.Core.Infrastructure;
using DH.Entity;
using DH.Services.Localization;

using Microsoft.Extensions.Logging;

using NewLife.Log;

using XCode.Membership;

namespace DH.Services.ScheduleTasks {
    /// <summary>
    /// 计划任务运行程序
    /// </summary>
    public partial class ScheduleTaskRunner : IScheduleTaskRunner
    {
        #region Fields

        protected readonly ILocalizationService _localizationService;
        protected readonly ILocker _locker;
        protected readonly ILogger _logger;
        protected readonly IStoreContext _storeContext;

        #endregion

        #region Ctor

        public ScheduleTaskRunner(ILocalizationService localizationService,
            ILocker locker,
            ILogger logger,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _locker = locker;
            _logger = logger;
            _storeContext = storeContext;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 初始化并执行任务
        /// </summary>
        protected async Task PerformTaskAsync(ScheduleTask scheduleTask)
        {
            var type = Type.GetType(scheduleTask.Type) ??
                       // 确保仅指定类型名称时它工作正常（不需要完全限定的名称）
                       AppDomain.CurrentDomain.GetAssemblies()
                           .Select(a => a.GetType(scheduleTask.Type))
                           .FirstOrDefault(t => t != null);
            if (type == null)
                throw new Exception($"Schedule task ({scheduleTask.Type}) cannot by instantiated");

            object instance = null;

            try
            {
                instance = EngineContext.Current.Resolve(type);
            }
            catch
            {
                // 忽略
            }

            instance ??= EngineContext.Current.ResolveUnregistered(type);

            if (instance is not IScheduleTask task)
                return;

            scheduleTask.LastStartUtc = DateTime.UtcNow;
            // 更新适当的日期时间属性
            scheduleTask.Update();
            await task.ExecuteAsync();
            scheduleTask.LastEndUtc = scheduleTask.LastSuccessUtc = DateTime.UtcNow;
            // 更新适当的日期时间属性
            scheduleTask.Update();
        }

        /// <summary>
        /// 任务是否已在运行？
        /// </summary>
        /// <param name="scheduleTask">计划任务</param>
        /// <returns>Result</returns>
        protected virtual bool IsTaskAlreadyRunning(ScheduleTask scheduleTask)
        {
            // 任务首次运行
            if (scheduleTask.LastStartUtc <= DateTime.MinValue && scheduleTask.LastEndUtc <= DateTime.MinValue)
                return false;

            var lastStartUtc = scheduleTask.LastStartUtc <= DateTime.MinValue ? DateTime.UtcNow : scheduleTask.LastStartUtc;

            // 任务已完成
            if (scheduleTask.LastEndUtc > DateTime.MinValue && lastStartUtc < scheduleTask.LastEndUtc)
                return false;

            // 上次任务未完成
            if (lastStartUtc.AddSeconds(scheduleTask.Seconds) <= DateTime.UtcNow)
                return false;

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="scheduleTask">计划任务</param>
        /// <param name="forceRun">强制运行</param>
        /// <param name="throwException">一个值，指示发生错误时是否应引发异常</param>
        /// <param name="ensureRunOncePerPeriod">一个值，指示是否应确保此任务在每个运行周期运行一次</param>
        public async Task ExecuteAsync(ScheduleTask scheduleTask, bool forceRun = false, bool throwException = false, bool ensureRunOncePerPeriod = true)
        {
            var enabled = forceRun || (scheduleTask?.Enabled ?? false);

            if (scheduleTask == null || !enabled)
                return;

            if (ensureRunOncePerPeriod)
            {
                // 任务已在运行
                if (IsTaskAlreadyRunning(scheduleTask))
                    return;

                // 验证（这样其他人都不能在需要时调用此方法）
                if (scheduleTask.LastStartUtc > DateTime.MinValue && (DateTime.UtcNow - scheduleTask.LastStartUtc).TotalSeconds < scheduleTask.Seconds)
                    // too early
                    return;
            }

            try
            {
                // 获取过期时间
                var expirationInSeconds = Math.Min(scheduleTask.Seconds, 300) - 1;
                var expiration = TimeSpan.FromSeconds(expirationInSeconds);

                // 带锁执行任务
                await _locker.PerformActionWithLockAsync(scheduleTask.Type, expiration, () => PerformTaskAsync(scheduleTask));
            }
            catch (Exception exc)
            {
                var store = _storeContext.CurrentStore;

                var scheduleTaskUrl = $"{store.Url}{DHTaskDefaults.ScheduleTaskPath}";

                scheduleTask.Enabled = !scheduleTask.StopOnError;
                scheduleTask.LastEndUtc = DateTime.UtcNow;
                scheduleTask.Update();

                var message = string.Format(_localizationService.GetResource("ScheduleTasks.Error"), scheduleTask.Name,
                    exc.Message, scheduleTask.Type, store.SiteName, scheduleTaskUrl);

                var webHelper = EngineContext.Current.Resolve<IWebHelper>();

                // 获取当前客户
                var currentCustomer = EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer;

                // 错误日志
                XTrace.WriteException(exc);
                LogProvider.Provider?.WriteLog("计划任务", "错误", false, message + " " + Environment.NewLine + exc.GetMessage(), currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());

                if (throwException)
                    throw;
            }
        }

        #endregion
    }
}
