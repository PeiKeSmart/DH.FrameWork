using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossContractGeneralCreateResponse.
    /// </summary>
    public class AlipayBossContractGeneralCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 发起审批返回结果
        /// </summary>
        [JsonPropertyName("result_set")]
        public InterTradetContractOpenApiStartResult ResultSet { get; set; }
    }
}
