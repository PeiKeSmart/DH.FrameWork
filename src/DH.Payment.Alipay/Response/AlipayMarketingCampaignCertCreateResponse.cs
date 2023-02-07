﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCampaignCertCreateResponse.
    /// </summary>
    public class AlipayMarketingCampaignCertCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 凭证id
        /// </summary>
        [JsonPropertyName("lot_number")]
        public string LotNumber { get; set; }
    }
}
