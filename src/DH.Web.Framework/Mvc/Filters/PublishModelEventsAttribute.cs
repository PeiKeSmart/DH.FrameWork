using DH.Core.Events;
using DH.Web.Framework.Events;
using DH.Web.Framework.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Net;

namespace DH.Web.Framework.Mvc.Filters
{
    /// <summary>
    /// 表示在操作执行之前、模型绑定完成之后发布ModelReceived事件的筛选器属性
    /// 并在操作执行之后、操作结果之前发布ModelPrepared事件
    /// </summary>
    public sealed class PublishModelEventsAttribute : TypeFilterAttribute
    {
        #region Ctor

        /// <summary>
        /// 创建筛选器属性的实例
        /// </summary>
        /// <param name="ignore">是否忽略筛选器操作的执行</param>
        public PublishModelEventsAttribute(bool ignore = false) : base(typeof(PublishModelEventsFilter))
        {
            IgnoreFilter = ignore;
            Arguments = new object[] { ignore };
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取一个值，该值指示是否忽略筛选器操作的执行
        /// </summary>
        public bool IgnoreFilter { get; }

        #endregion

        #region Nested filter

        /// <summary>
        /// 表示在操作执行之前、模型绑定完成之后发布ModelReceived事件的筛选器
        /// 并在操作执行之后、操作结果之前发布ModelPrepared事件
        /// </summary>
        private class PublishModelEventsFilter : IAsyncActionFilter, IAsyncResultFilter
        {
            #region Fields

            private readonly bool _ignoreFilter;
            private readonly IEventPublisher _eventPublisher;

            #endregion

            #region Ctor

            public PublishModelEventsFilter(bool ignoreFilter,
                IEventPublisher eventPublisher)
            {
                _ignoreFilter = ignoreFilter;
                _eventPublisher = eventPublisher;
            }

            #endregion

            #region Utilities

            /// <summary>
            /// 是否忽略此筛选器
            /// </summary>
            /// <param name="context">操作筛选器的上下文</param>
            /// <returns>结果</returns>
            protected virtual bool IgnoreFilter(FilterContext context)
            {
                // 检查此筛选器是否已为操作覆盖
                var actionFilter = context.ActionDescriptor.FilterDescriptors
                    .Where(filterDescriptor => filterDescriptor.Scope == FilterScope.Action)
                    .Select(filterDescriptor => filterDescriptor.Filter)
                    .OfType<PublishModelEventsAttribute>()
                    .FirstOrDefault();

                return actionFilter?.IgnoreFilter ?? _ignoreFilter;
            }

            /// <summary>
            /// 发布模型准备事件
            /// </summary>
            /// <param name="model">模型</param>
            /// <returns>表示异步操作的任务</returns>
            protected virtual async Task PublishModelPreparedEventAsync(object model)
            {
                // 我们将所有模型的ModelPrepared事件发布为BaseDHModel
                // 因此需要实现IConsumer<ModelPrepared<BaseDHModel>>接口来处理此事件
                if (model is BaseDHModel dhModel)
                    await _eventPublisher.ModelPreparedAsync(dhModel);

                // 我们将用于收集的ModelPrepared事件发布为IEnumerable<BaseDHModel>, 
                // 因此，您需要实现IConsumer<ModelPrepared<IEnumerable<BaseDHModel>>>接口来处理此事件
                if (model is IEnumerable<BaseDHModel> dhModelCollection)
                    await _eventPublisher.ModelPreparedAsync(dhModelCollection);
            }

            /// <summary>
            /// 在操作之前、模型绑定完成之后异步调用。
            /// </summary>
            /// <param name="context">操作筛选器的上下文</param>
            /// <returns>表示异步操作的任务</returns>
            private async Task PublishModelReceivedEventAsync(ActionExecutingContext context)
            {
                if (context == null)
                    throw new ArgumentNullException(nameof(context));

                // 仅在POST请求中
                if (!context.HttpContext.Request.Method.Equals(WebRequestMethods.Http.Post, StringComparison.InvariantCultureIgnoreCase))
                    return;

                if (IgnoreFilter(context))
                    return;

                // 模型接收事件
                foreach (var model in context.ActionArguments.Values.OfType<BaseDHModel>())
                {
                    // 我们将所有模型的ModelReceived事件发布为BaseDHModel, 
                    // 因此需要实现IConsumer<ModelReceived<BaseDHModel>>接口来处理此事件
                    await _eventPublisher.ModelReceivedAsync(model, context.ModelState);
                }
            }

            /// <summary>
            /// 在操作之前、模型绑定完成之后异步调用.
            /// </summary>
            /// <param name="context">操作筛选器的上下文</param>
            /// <returns>表示异步操作的任务</returns>
            private async Task PublishModelPreparedEventAsync(ActionExecutingContext context)
            {
                if (context == null)
                    throw new ArgumentNullException(nameof(context));

                if (IgnoreFilter(context))
                    return;

                // 模型准备事件
                if (context.Controller is Controller controller)
                    await PublishModelPreparedEventAsync(controller.ViewData.Model);
            }

            #endregion

            #region Methods

            /// <summary>
            /// 在操作之前、模型绑定完成之后异步调用。
            /// </summary>
            /// <param name="context">操作筛选器的上下文</param>
            /// <param name="next">被调用以执行下一个动作筛选器或动作本身的委托</param>
            /// <returns>表示异步操作的任务</returns>
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                await PublishModelReceivedEventAsync(context);
                if (context.Result == null)
                    await next();
                await PublishModelPreparedEventAsync(context);
            }

            /// <summary>在操作结果之前异步调用。</summary>
            /// <param name="context">操作筛选器的上下文</param>
            /// <param name="next">被调用以执行下一个动作筛选器或动作本身的委托</param>
            /// <returns>表示异步操作的任务</returns>
            public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
            {
                if (context == null)
                    throw new ArgumentNullException(nameof(context));

                if (IgnoreFilter(context))
                    return;

                // 模型准备事件
                if (context.Result is JsonResult result)
                    await PublishModelPreparedEventAsync(result.Value);

                await next();
            }

            #endregion
        }

        #endregion
    }
}
