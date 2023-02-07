using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiServindustryPromoGuidequotaQueryResponse.
    /// </summary>
    public class KoubeiServindustryPromoGuidequotaQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 权限份额
        /// </summary>
        [JsonPropertyName("auth_quota")]
        public List<IgAuthQuota> AuthQuota { get; set; }
    }
}
