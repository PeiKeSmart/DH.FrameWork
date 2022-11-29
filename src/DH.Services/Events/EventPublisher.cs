using DH.Core;
using DH.Core.Events;
using DH.Core.Infrastructure;
using NewLife.Log;

using XCode.Membership;

namespace DH.Services.Events
{
    /// <summary>
    /// 表示事件发布者实现
    /// </summary>
    public partial class EventPublisher : IEventPublisher
    {
        #region Methods

        /// <summary>
        /// 向消费者发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">Event对象</param>
        /// <returns>表示异步操作的任务</returns>
        public virtual async Task PublishAsync<TEvent>(TEvent @event)
        {
            // 获取所有事件消费者
            var consumers = EngineContext.Current.ResolveAll<IConsumer<TEvent>>().ToList();

            foreach (var consumer in consumers)
            {
                try
                {
                    // 尝试处理已发布事件
                    await consumer.HandleEventAsync(@event);
                }
                catch (Exception exception)
                {
                    // 日志错误，我们放入嵌套的try-catch以防止可能的循环（如果发生错误）
                    try
                    {
                        var webHelper = EngineContext.Current.Resolve<IWebHelper>();

                        // 获取当前客户
                        var currentCustomer = EngineContext.Current.Resolve<IWorkContext>().GetCurrentCustomer();

                        // 错误日志
                        XTrace.WriteException(exception);
                        LogProvider.Provider?.WriteLog("事件消费者", "错误", false, exception.Message + " " + Environment.NewLine + exception.GetMessage(), currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }

        /// <summary>
        /// 向消费者发布活动
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件对象</param>
        public virtual void Publish<TEvent>(TEvent @event)
        {
            //获取所有事件消费者
            var consumers = EngineContext.Current.ResolveAll<IConsumer<TEvent>>().ToList();

            foreach (var consumer in consumers)
            {
                try
                {
                    // 尝试处理已发布的事件
                    consumer.HandleEvent(@event);
                }
                catch (Exception exception)
                {
                    // 记录错误，我们放入嵌套try-catch以防止可能的循环（如果发生某些错误）
                    try
                    {
                        XTrace.WriteException(exception);
                    }
                    catch
                    {
                        // 忽略
                    }
                }
            }
        }

        #endregion
    }
}
