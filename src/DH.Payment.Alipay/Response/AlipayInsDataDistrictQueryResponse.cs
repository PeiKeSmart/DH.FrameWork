﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayInsDataDistrictQueryResponse.
    /// </summary>
    public class AlipayInsDataDistrictQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 地区信息
        /// </summary>
        [JsonPropertyName("districts")]
        public string Districts { get; set; }
    }
}
