﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiItemExtitemInfoCreateResponse.
    /// </summary>
    public class KoubeiItemExtitemInfoCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 创建成功，返回id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
