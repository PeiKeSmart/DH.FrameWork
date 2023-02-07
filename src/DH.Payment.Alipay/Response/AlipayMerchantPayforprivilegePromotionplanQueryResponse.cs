using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMerchantPayforprivilegePromotionplanQueryResponse.
    /// </summary>
    public class AlipayMerchantPayforprivilegePromotionplanQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 充值方案信息，返回之前创建的充值方案信息，方案不存在或者无权限返回空
        /// </summary>
        [JsonPropertyName("promotion_plan")]
        public PayForPrivilegePromotionPlanInfo PromotionPlan { get; set; }
    }
}
