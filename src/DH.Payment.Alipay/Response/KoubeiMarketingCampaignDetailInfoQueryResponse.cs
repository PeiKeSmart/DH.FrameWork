﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignDetailInfoQueryResponse.
    /// </summary>
    public class KoubeiMarketingCampaignDetailInfoQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 适用门店:门店与门店之间用“,”隔开
        /// </summary>
        [JsonPropertyName("limit_shops")]
        public string LimitShops { get; set; }
    }
}
