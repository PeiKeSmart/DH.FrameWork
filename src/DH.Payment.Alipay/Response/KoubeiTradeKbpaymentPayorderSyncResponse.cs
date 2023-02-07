using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiTradeKbpaymentPayorderSyncResponse.
    /// </summary>
    public class KoubeiTradeKbpaymentPayorderSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 请求id
        /// </summary>
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }
    }
}
