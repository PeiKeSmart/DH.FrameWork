﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// NameOuterIdPair Data Structure.
    /// </summary>
    public class NameOuterIdPair : AlipayObject
    {
        /// <summary>
        /// 属性或者规格的名称
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 外部ID
        /// </summary>
        [JsonPropertyName("outer_id")]
        public string OuterId { get; set; }
    }
}
