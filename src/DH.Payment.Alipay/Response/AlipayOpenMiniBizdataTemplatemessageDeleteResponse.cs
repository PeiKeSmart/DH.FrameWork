﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniBizdataTemplatemessageDeleteResponse.
    /// </summary>
    public class AlipayOpenMiniBizdataTemplatemessageDeleteResponse : AlipayResponse
    {
        /// <summary>
        /// 成功：true 失败：false
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
