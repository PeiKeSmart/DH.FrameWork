namespace DH.Services.Events;

/// <summary>
/// 表示模型准备查看后发生的事件
/// </summary>
/// <typeparam name="T">模型的类型</typeparam>
public partial class ModelPreparedEvent<T> {
    #region Ctor

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="model">Model</param>
    public ModelPreparedEvent(T model)
    {
        Model = model;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets a model
    /// </summary>
    public T Model { get; private set; }

    #endregion
}