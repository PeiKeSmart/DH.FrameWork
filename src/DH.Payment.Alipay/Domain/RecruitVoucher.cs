using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// RecruitVoucher Data Structure.
    /// </summary>
    public class RecruitVoucher : AlipayObject
    {
        /// <summary>
        /// 券活动id
        /// </summary>
        [JsonPropertyName("activity_id")]
        public string ActivityId { get; set; }
    }
}
