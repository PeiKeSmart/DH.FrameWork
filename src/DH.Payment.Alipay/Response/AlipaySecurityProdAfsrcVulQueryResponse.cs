﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySecurityProdAfsrcVulQueryResponse.
    /// </summary>
    public class AlipaySecurityProdAfsrcVulQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 漏洞信息
        /// </summary>
        [JsonPropertyName("data")]
        public VulInfo Data { get; set; }
    }
}
