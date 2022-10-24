using DH.Services.ScheduleTasks;

namespace DH.Services.Common
{
    /// <summary>
    /// 表示用于保持站点活动的任务
    /// </summary>
    public partial class KeepAliveTask : IScheduleTask
    {
        #region Fields

        private readonly StoreHttpClient _storeHttpClient;

        #endregion

        #region Ctor

        public KeepAliveTask(StoreHttpClient storeHttpClient)
        {
            _storeHttpClient = storeHttpClient;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 执行任务
        /// </summary>
        public async System.Threading.Tasks.Task ExecuteAsync()
        {
            await _storeHttpClient.KeepAliveAsync();
        }

        #endregion
    }
}
