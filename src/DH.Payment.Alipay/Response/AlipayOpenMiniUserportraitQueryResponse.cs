﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniUserportraitQueryResponse.
    /// </summary>
    public class AlipayOpenMiniUserportraitQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 用户画像
        /// </summary>
        [JsonPropertyName("portrait_distributions")]
        public List<PortraitDistribution> PortraitDistributions { get; set; }
    }
}
