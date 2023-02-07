using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayBossFncGfsettlePaycontractCloseModel Data Structure.
    /// </summary>
    public class AlipayBossFncGfsettlePaycontractCloseModel : AlipayObject
    {
        /// <summary>
        /// 付款条款基础信息
        /// </summary>
        [JsonPropertyName("paycontractbasedto")]
        public PayContractBaseDTO Paycontractbasedto { get; set; }
    }
}
