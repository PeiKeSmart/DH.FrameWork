﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiMarketingCampaignMemberAuthApplyModel Data Structure.
    /// </summary>
    public class KoubeiMarketingCampaignMemberAuthApplyModel : AlipayObject
    {
        /// <summary>
        /// token
        /// </summary>
        [JsonPropertyName("auth_token")]
        public string AuthToken { get; set; }
    }
}
