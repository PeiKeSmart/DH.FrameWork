namespace DH.Core.Events
{
    /// <summary>
    /// 表示事件发布者
    /// </summary>
    public partial interface IEventPublisher
    {
        /// <summary>
        /// 向消费者发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件对象</param>
        Task PublishAsync<TEvent>(TEvent @event);

        /// <summary>
        /// 向消费者发布活动
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件对象</param>
        void Publish<TEvent>(TEvent @event);
    }
}
