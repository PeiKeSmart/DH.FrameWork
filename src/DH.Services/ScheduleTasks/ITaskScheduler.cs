namespace DH.Services.ScheduleTasks
{
    /// <summary>
    /// 任务管理器界面
    /// </summary>
    public interface ITaskScheduler
    {
        /// <summary>
        /// 初始化任务计划程序
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// 启动任务计划程序
        /// </summary>
        public void StartScheduler();

        /// <summary>
        /// 停止任务计划程序
        /// </summary>
        public void StopScheduler();
    }
}
