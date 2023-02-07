using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySecurityProdAlipaySecurityProdTestResponse.
    /// </summary>
    public class AlipaySecurityProdAlipaySecurityProdTestResponse : AlipayResponse
    {
        /// <summary>
        /// ddd
        /// </summary>
        [JsonPropertyName("admin")]
        public string Admin { get; set; }
    }
}
