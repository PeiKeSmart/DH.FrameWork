using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceTransportCarrentalServiceSubmitResponse.
    /// </summary>
    public class AlipayCommerceTransportCarrentalServiceSubmitResponse : AlipayResponse
    {
        /// <summary>
        /// 请求ID
        /// </summary>
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }
    }
}
