using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceAntestCaselistQueryResponse.
    /// </summary>
    public class AlipayCommerceAntestCaselistQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 用例列表JSONString
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}
