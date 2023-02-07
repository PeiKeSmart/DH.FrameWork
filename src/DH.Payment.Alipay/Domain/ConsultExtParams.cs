﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// ConsultExtParams Data Structure.
    /// </summary>
    public class ConsultExtParams : AlipayObject
    {
        /// <summary>
        /// key-value
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}
