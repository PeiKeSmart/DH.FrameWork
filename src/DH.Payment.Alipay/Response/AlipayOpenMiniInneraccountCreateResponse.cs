﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniInneraccountCreateResponse.
    /// </summary>
    public class AlipayOpenMiniInneraccountCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 虚拟PID
        /// </summary>
        [JsonPropertyName("pid")]
        public string Pid { get; set; }
    }
}
