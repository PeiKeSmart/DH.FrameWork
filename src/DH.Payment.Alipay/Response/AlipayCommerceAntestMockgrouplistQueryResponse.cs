using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceAntestMockgrouplistQueryResponse.
    /// </summary>
    public class AlipayCommerceAntestMockgrouplistQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 挡板集列表
        /// </summary>
        [JsonPropertyName("data")]
        public List<EcoMockGroupInfo> Data { get; set; }
    }
}
