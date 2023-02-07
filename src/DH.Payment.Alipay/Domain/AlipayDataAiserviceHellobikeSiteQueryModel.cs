using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayDataAiserviceHellobikeSiteQueryModel Data Structure.
    /// </summary>
    public class AlipayDataAiserviceHellobikeSiteQueryModel : AlipayObject
    {
        /// <summary>
        /// 任务id.  （当空时，返回最近3个月的所有任务及状态）
        /// </summary>
        [JsonPropertyName("plan_id")]
        public string PlanId { get; set; }
    }
}
