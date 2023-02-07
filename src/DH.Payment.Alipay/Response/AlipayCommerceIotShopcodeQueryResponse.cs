using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceIotShopcodeQueryResponse.
    /// </summary>
    public class AlipayCommerceIotShopcodeQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 投放状态，已投放（on），未投放（off）
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
