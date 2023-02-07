using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataDataserviceAdPlanQueryResponse.
    /// </summary>
    public class AlipayDataDataserviceAdPlanQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 计划详情
        /// </summary>
        [JsonPropertyName("plan_detail")]
        public OuterPlan PlanDetail { get; set; }
    }
}
