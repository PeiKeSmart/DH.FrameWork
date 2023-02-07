﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiMarketingCampaignBenefitQueryModel Data Structure.
    /// </summary>
    public class KoubeiMarketingCampaignBenefitQueryModel : AlipayObject
    {
        /// <summary>
        /// 权益id
        /// </summary>
        [JsonPropertyName("benefit_id")]
        public string BenefitId { get; set; }
    }
}
