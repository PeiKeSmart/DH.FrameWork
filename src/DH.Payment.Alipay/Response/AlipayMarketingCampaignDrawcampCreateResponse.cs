﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCampaignDrawcampCreateResponse.
    /// </summary>
    public class AlipayMarketingCampaignDrawcampCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 抽奖活动id
        /// </summary>
        [JsonPropertyName("camp_id")]
        public string CampId { get; set; }
    }
}
