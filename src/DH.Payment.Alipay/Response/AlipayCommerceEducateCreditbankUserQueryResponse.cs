using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateCreditbankUserQueryResponse.
    /// </summary>
    public class AlipayCommerceEducateCreditbankUserQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 学分银行id
        /// </summary>
        [JsonPropertyName("cb_id")]
        public string CbId { get; set; }
    }
}
