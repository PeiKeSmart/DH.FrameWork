﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicMessageQueryResponse.
    /// </summary>
    public class AlipayOpenPublicMessageQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 发送消息结果集。仅返回该用户已发送的消息
        /// </summary>
        [JsonPropertyName("list")]
        public List<PublicMessageInfo> List { get; set; }
    }
}
