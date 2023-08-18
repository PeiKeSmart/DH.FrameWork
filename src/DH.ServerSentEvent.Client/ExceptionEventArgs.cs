namespace DH.ServerSentEvent;

/// <summary>
/// 提供在事件源 <see cref="EventSource.Error" /> 事件中引发的异常数据。
/// </summary>
/// <seealso cref="System.EventArgs" />
public class ExceptionEventArgs : EventArgs {
    /// <summary>
    /// Gets the <see cref="System.Exception"/> that represents the error that occurred.
    /// </summary>
    /// <value>
    /// The exception.
    /// </value>
    public Exception Exception { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionEventArgs"/> class.
    /// </summary>
    /// <param name="ex">A <see cref="System.Exception"/> that represents the error that occurred.</param>
    public ExceptionEventArgs(Exception ex)
    {
        Exception = ex;
    }
}