namespace DH.ServerSentEvent;

/// <summary>
/// 提供 <see cref="EventSource"/> 连接状态的数据。
/// </summary>
/// <seealso cref="System.EventArgs" />
public class StateChangedEventArgs : EventArgs {
    /// <summary>
    /// Gets the state of the EventSource connection.
    /// </summary>
    /// <value>
    /// One of the <see cref="EventSource.ReadyState"/> values, which represents the state of the EventSource connection.
    /// </value>
    public ReadyState ReadyState { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StateChangedEventArgs"/> class.
    /// </summary>
    /// <param name="readyState">One of the <see cref="EventSource.ReadyState"/> values, which represents the state of the EventSource connection.</param>
    public StateChangedEventArgs(ReadyState readyState)
    {
        ReadyState = readyState;
    }
}