using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySecurityProdAfsrcWhitehatinfoQueryResponse.
    /// </summary>
    public class AlipaySecurityProdAfsrcWhitehatinfoQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 白帽子信息
        /// </summary>
        [JsonPropertyName("data")]
        public WhitehatInfo Data { get; set; }
    }
}
