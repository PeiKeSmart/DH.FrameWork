﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenBpaasServiceQueryResponse.
    /// </summary>
    public class AlipayOpenBpaasServiceQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 服务列表
        /// </summary>
        [JsonPropertyName("service_list")]
        public List<BPaaSServiceInfo> ServiceList { get; set; }
    }
}
