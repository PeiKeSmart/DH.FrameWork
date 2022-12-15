namespace DH.Pdu.Encoder;

/// <summary>
/// 指定手机是否存储或丢弃消息指示
/// </summary>
public enum MessageIndicationOperation
{
    /// <summary>
    /// 消息指示操作未使用 - 默认
    /// </summary>
    NotSet,
    /// <summary>
    /// 存储指示消息
    /// </summary>
    Store,
    /// <summary>
    /// 丢弃指示消息
    /// </summary>
    Discard
}
