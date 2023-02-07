﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignIntelligentShopConsultResponse.
    /// </summary>
    public class KoubeiMarketingCampaignIntelligentShopConsultResponse : AlipayResponse
    {
        /// <summary>
        /// 总共项数
        /// </summary>
        [JsonPropertyName("items")]
        public string Items { get; set; }

        /// <summary>
        /// 智能营销方案符合标准的门店列表
        /// </summary>
        [JsonPropertyName("shops")]
        public List<IntelligentPromoShopSummaryInfo> Shops { get; set; }
    }
}
