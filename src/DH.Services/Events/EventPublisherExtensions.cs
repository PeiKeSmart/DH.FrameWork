using DH.Core.Events;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DH.Services.Events;

/// <summary>
/// 表示事件发布者扩展
/// </summary>
public static class EventPublisherExtensions {
    /// <summary>
    /// 发布ModelPrepared事件
    /// </summary>
    /// <typeparam name="T">模型类型</typeparam>
    /// <param name="eventPublisher">事件发布者</param>
    /// <param name="model">模型</param>
    /// <returns>表示异步操作的任务</returns>
    public static async Task ModelPreparedAsync<T>(this IEventPublisher eventPublisher, T model)
    {
        await eventPublisher.PublishAsync(new ModelPreparedEvent<T>(model));
    }

    /// <summary>
    /// 发布ModelReceived事件
    /// </summary>
    /// <typeparam name="T">模型类型</typeparam>
    /// <param name="eventPublisher">事件发布者</param>
    /// <param name="model">模型</param>
    /// <param name="modelState">模型状态</param>
    /// <returns>表示异步操作的任务</returns>
    public static async Task ModelReceivedAsync<T>(this IEventPublisher eventPublisher, T model, ModelStateDictionary modelState)
    {
        await eventPublisher.PublishAsync(new ModelReceivedEvent<T>(model, modelState));
    }
}