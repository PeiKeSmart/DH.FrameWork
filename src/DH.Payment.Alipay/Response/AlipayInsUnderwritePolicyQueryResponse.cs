using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayInsUnderwritePolicyQueryResponse.
    /// </summary>
    public class AlipayInsUnderwritePolicyQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 保单
        /// </summary>
        [JsonPropertyName("policy")]
        public InsPolicy Policy { get; set; }
    }
}
