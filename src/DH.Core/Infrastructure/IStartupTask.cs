namespace DH.Core.Infrastructure
{
    /// <summary>
    /// 应该由启动时运行的任务实现的接口
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task ExecuteAsync();

        /// <summary>
        /// 获取此启动任务实现的顺序
        /// </summary>
        int Order { get; }
    }
}
