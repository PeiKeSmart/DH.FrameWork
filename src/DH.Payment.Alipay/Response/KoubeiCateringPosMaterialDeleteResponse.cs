﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringPosMaterialDeleteResponse.
    /// </summary>
    public class KoubeiCateringPosMaterialDeleteResponse : AlipayResponse
    {
        /// <summary>
        /// 配料id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
