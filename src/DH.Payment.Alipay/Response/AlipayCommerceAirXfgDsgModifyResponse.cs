using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceAirXfgDsgModifyResponse.
    /// </summary>
    public class AlipayCommerceAirXfgDsgModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 用户级别
        /// </summary>
        [JsonPropertyName("level")]
        public string Level { get; set; }
    }
}
