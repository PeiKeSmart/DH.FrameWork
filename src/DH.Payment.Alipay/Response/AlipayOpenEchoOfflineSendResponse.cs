﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenEchoOfflineSendResponse.
    /// </summary>
    public class AlipayOpenEchoOfflineSendResponse : AlipayResponse
    {
        /// <summary>
        /// 10000
        /// </summary>
        [JsonPropertyName("word")]
        public string Word { get; set; }
    }
}
