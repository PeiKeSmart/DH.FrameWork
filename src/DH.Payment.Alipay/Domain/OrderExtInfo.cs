﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// OrderExtInfo Data Structure.
    /// </summary>
    public class OrderExtInfo : AlipayObject
    {
        /// <summary>
        /// 键值
        /// </summary>
        [JsonPropertyName("ext_key")]
        public string ExtKey { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [JsonPropertyName("ext_value")]
        public string ExtValue { get; set; }
    }
}
