namespace DH.Services.Events
{
    /// <summary>
    /// 消费者接口
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public interface IConsumer<T>
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="eventMessage">事件</param>
        /// <returns>表示异步操作的任务</returns>
        Task HandleEventAsync(T eventMessage);
    }
}
