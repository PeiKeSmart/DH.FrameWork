﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenLotteryRegionCreateResponse.
    /// </summary>
    public class AlipayOpenLotteryRegionCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 商家入驻ID
        /// </summary>
        [JsonPropertyName("region_id")]
        public string RegionId { get; set; }
    }
}
