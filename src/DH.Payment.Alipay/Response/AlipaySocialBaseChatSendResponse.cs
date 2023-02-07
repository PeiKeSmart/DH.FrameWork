using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySocialBaseChatSendResponse.
    /// </summary>
    public class AlipaySocialBaseChatSendResponse : AlipayResponse
    {
        /// <summary>
        /// msg_index:msgid+sessionId
        /// </summary>
        [JsonPropertyName("msg_index")]
        public string MsgIndex { get; set; }
    }
}
