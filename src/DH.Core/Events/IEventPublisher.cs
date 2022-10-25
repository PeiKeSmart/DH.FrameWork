namespace DH.Core.Events
{
    /// <summary>
    /// 表示事件发布者
    /// </summary>
    public partial interface IEventPublisher
    {
        /// <summary>
        /// Publish event to consumers
        /// </summary>
        /// <typeparam name="TEvent">Type of event</typeparam>
        /// <param name="event">Event object</param>
        Task PublishAsync<TEvent>(TEvent @event);
    }
}
