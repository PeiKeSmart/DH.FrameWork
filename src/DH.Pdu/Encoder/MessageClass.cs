namespace DH.Pdu.Encoder;

/// <summary>
/// 消息类指示将存储消息的位置
/// </summary>
public enum MessageClass
{
    /// <summary>
    /// Flash消息仅显示
    /// </summary>
    ImmediateDisplay,
    /// <summary>
    /// 默认商店
    /// </summary>
    MESpecific,
    /// <summary>
    /// SIM的消息
    /// </summary>
    SIMSpecific,
    /// <summary>
    /// TE具体
    /// </summary>
    TESpecific
}
