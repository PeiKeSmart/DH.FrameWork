﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayIserviceCcmSwArticleCreateResponse.
    /// </summary>
    public class AlipayIserviceCcmSwArticleCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
