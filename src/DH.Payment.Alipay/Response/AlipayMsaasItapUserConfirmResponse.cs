﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMsaasItapUserConfirmResponse.
    /// </summary>
    public class AlipayMsaasItapUserConfirmResponse : AlipayResponse
    {
        /// <summary>
        /// Itap统一返回结果模型
        /// </summary>
        [JsonPropertyName("payload")]
        public List<ItapResponsePayload> Payload { get; set; }

        /// <summary>
        /// 请求唯一ID
        /// </summary>
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }
    }
}
