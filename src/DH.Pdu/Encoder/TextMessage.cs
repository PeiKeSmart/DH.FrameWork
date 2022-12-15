using System.Text;

namespace DG.Pdu.Encoder
{
    /// <summary>
    /// 标准纯文本消息
    /// </summary>
    public class TextMessage : ISmsMessageContent
    {
        /// <summary>
        /// 消息的文本内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 数据编码（7位，8位，16位）
        /// </summary>
        public DataEncoding DataEncoding { get; set; }

        /// <summary>
        /// 实例化
        /// </summary>
        public TextMessage()
        {
            this.DataEncoding = DataEncoding.Default7bit;
            this.Text = string.Empty;
        }

        /// <summary>
        /// 返回适合在SMS消息中发送的字节数组。 包括所有用户标头（如果有）
        /// </summary>
        /// <returns>字节数组</returns>
        public byte[] GetSMSBytes()
        {
            return GetDataBytes();
        }

        /// <summary>
        /// 方法返回用户头字节。 在纯文本消息中，它总是空的
        /// </summary>
        /// <returns></returns>
        public byte[] GetUDHBytes()
        {
            return new byte[] { };
        }

        /// <summary>
        /// 返回以DataEncoding编码的消息文本
        /// </summary>
        /// <returns></returns>
        protected byte[] GetDataBytes()
        {
            var data = new byte[] { };

            switch (DataEncoding)
            {
                case DataEncoding.Default7bit:
                    data = Encoding.ASCII.GetBytes(Text);
                    break;

                case DataEncoding.Data8bit:
                    data = Encoding.GetEncoding("iso-8859-1").GetBytes(Text);
                    break;

                case DataEncoding.UCS2_16bit:
                    data = Encoding.BigEndianUnicode.GetBytes(Text);
                    break;
            }

            return data;
        }
    }
}
