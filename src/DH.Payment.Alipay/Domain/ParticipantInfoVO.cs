﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// ParticipantInfoVO Data Structure.
    /// </summary>
    public class ParticipantInfoVO : AlipayObject
    {
        /// <summary>
        /// 参与者名称类型
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// 参与者名称
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
