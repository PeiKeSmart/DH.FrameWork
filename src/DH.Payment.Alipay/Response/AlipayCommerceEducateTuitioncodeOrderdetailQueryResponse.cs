using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateTuitioncodeOrderdetailQueryResponse.
    /// </summary>
    public class AlipayCommerceEducateTuitioncodeOrderdetailQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 对象
        /// </summary>
        [JsonPropertyName("data")]
        public RechargeOrderTuitionDTO Data { get; set; }
    }
}
