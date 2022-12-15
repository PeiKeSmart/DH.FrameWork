namespace DH.Pdu.Encoder;

/// <summary>
/// SMS消息编码
/// </summary>
public enum DataEncoding
{
    /// <summary>
    /// GSM中默认使用7位编码
    /// </summary>
    Default7bit,
    /// <summary>
    /// 8位编码
    /// </summary>
    Data8bit,
    /// <summary>
    /// UCS2 16位编码
    /// </summary>
    UCS2_16bit
}
