﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipaySocialBaseChatGroupsQueryModel Data Structure.
    /// </summary>
    public class AlipaySocialBaseChatGroupsQueryModel : AlipayObject
    {
        /// <summary>
        /// 上次接口返回的key，初始传0
        /// </summary>
        [JsonPropertyName("last_key")]
        public long LastKey { get; set; }
    }
}
