using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMobilePublicMessagebatchPushResponse.
    /// </summary>
    public class AlipayMobilePublicMessagebatchPushResponse : AlipayResponse
    {
        /// <summary>
        /// 成功
        /// </summary>
        [JsonPropertyName("code")]
        public new string Code { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        [JsonPropertyName("msg")]
        public new string Msg { get; set; }
    }
}
