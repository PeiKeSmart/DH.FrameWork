﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusTimeodGetResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusTimeodGetResponse : AlipayResponse
    {
        /// <summary>
        /// od分时结果列表
        /// </summary>
        [JsonPropertyName("result")]
        public List<CloudbusTimeOdItem> Result { get; set; }
    }
}
