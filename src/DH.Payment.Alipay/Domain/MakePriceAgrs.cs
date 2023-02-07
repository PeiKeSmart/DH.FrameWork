﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// MakePriceAgrs Data Structure.
    /// </summary>
    public class MakePriceAgrs : AlipayObject
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
