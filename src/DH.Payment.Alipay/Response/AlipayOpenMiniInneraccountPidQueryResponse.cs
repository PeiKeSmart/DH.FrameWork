﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniInneraccountPidQueryResponse.
    /// </summary>
    public class AlipayOpenMiniInneraccountPidQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 虚拟PID
        /// </summary>
        [JsonPropertyName("pid")]
        public string Pid { get; set; }
    }
}
