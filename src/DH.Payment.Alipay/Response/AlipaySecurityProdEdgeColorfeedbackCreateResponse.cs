using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySecurityProdEdgeColorfeedbackCreateResponse.
    /// </summary>
    public class AlipaySecurityProdEdgeColorfeedbackCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 反馈生成的跟踪字符串，用于后续业务问题排查
        /// </summary>
        [JsonPropertyName("trace")]
        public string Trace { get; set; }
    }
}
