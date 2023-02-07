﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySocialBaseGroupCreateResponse.
    /// </summary>
    public class AlipaySocialBaseGroupCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 群id
        /// </summary>
        [JsonPropertyName("group_id")]
        public string GroupId { get; set; }
    }
}
