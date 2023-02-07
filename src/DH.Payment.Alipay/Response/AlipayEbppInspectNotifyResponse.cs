﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppInspectNotifyResponse.
    /// </summary>
    public class AlipayEbppInspectNotifyResponse : AlipayResponse
    {
        /// <summary>
        /// 结果同步状态
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
