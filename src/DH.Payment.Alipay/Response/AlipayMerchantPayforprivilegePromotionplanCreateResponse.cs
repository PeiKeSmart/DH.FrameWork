using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMerchantPayforprivilegePromotionplanCreateResponse.
    /// </summary>
    public class AlipayMerchantPayforprivilegePromotionplanCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 充值方案，当前接口创建的充值方案
        /// </summary>
        [JsonPropertyName("promotion_plan")]
        public PayForPrivilegePromotionPlanInfo PromotionPlan { get; set; }
    }
}
