using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DH.Services.Events;

/// <summary>
/// 表示从视图接收模型后发生的事件
/// </summary>
/// <typeparam name="T">Type of the model</typeparam>
public partial class ModelReceivedEvent<T> {
    #region Ctor

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="modelState">Model state</param>
    public ModelReceivedEvent(T model, ModelStateDictionary modelState)
    {
        Model = model;
        ModelState = modelState;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets a model
    /// </summary>
    public T Model { get; private set; }

    /// <summary>
    /// Gets a model state
    /// </summary>
    public ModelStateDictionary ModelState { get; private set; }

    #endregion
}