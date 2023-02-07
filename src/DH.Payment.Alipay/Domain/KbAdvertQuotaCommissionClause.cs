using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// KbAdvertQuotaCommissionClause Data Structure.
    /// </summary>
    public class KbAdvertQuotaCommissionClause : AlipayObject
    {
        /// <summary>
        /// 固定金额
        /// </summary>
        [JsonPropertyName("quota_amount")]
        public string QuotaAmount { get; set; }
    }
}
