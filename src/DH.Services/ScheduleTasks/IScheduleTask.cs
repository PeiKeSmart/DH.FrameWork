namespace DH.Services.ScheduleTasks
{
    /// <summary>
    /// 每个任务应实现的接口
    /// </summary>
    public partial interface IScheduleTask
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        Task ExecuteAsync();
    }
}
