﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMemberBrandownerNameQueryResponse.
    /// </summary>
    public class KoubeiMemberBrandownerNameQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 品牌商名称
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
