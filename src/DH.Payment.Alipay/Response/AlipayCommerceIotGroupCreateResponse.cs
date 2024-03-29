﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceIotGroupCreateResponse.
    /// </summary>
    public class AlipayCommerceIotGroupCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 分组创建成功的id
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }
    }
}
