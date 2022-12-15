namespace DG.Pdu.Encoder
{
    /// <summary>
    /// SMS的主体内容接口
    /// </summary>
    public interface ISmsMessageContent
    {
        /// <summary>
        /// 数据编码（7位，8位，16位）
        /// </summary>
        DataEncoding DataEncoding { get; set; }

        /// <summary>
        /// 返回适合在SMS消息中发送的字节数组。 包括所有用户标头（如果有）
        /// </summary>
        /// <returns>字节数组</returns>
        byte[] GetSMSBytes();

        /// <summary>
        /// 返回用户标头的字节数组,(在纯文本消息中，它总是空的)
        /// </summary>
        /// <returns>字节数组</returns>
        byte[] GetUDHBytes();
    }
}
