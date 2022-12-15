namespace DH.Pdu.Encoder;

/// <summary>
/// 指示代表的消息类型
/// </summary>
public enum IndicationType
{
    /// <summary>
    /// 语音邮件留言等待
    /// </summary>
    Voicemail,
    /// <summary>
    /// 传真留言等待
    /// </summary>
    FaxMessage,
    /// <summary>
    /// 电子邮件等待
    /// </summary>
    EmailMessage,
    /// <summary>
    /// 其他留言等待
    /// </summary>
    OtherMessage
}
