﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignBenefitQueryResponse.
    /// </summary>
    public class KoubeiMarketingCampaignBenefitQueryResponse : AlipayResponse
    {
        /// <summary>
        /// benefitQueryInfo: 权益查询接口返回的具体权益信息
        /// </summary>
        [JsonPropertyName("benefit_query_info")]
        public BenefitQueryInfo BenefitQueryInfo { get; set; }
    }
}
