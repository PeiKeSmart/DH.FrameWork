﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayIserviceCcmSwArticleBatchqueryResponse.
    /// </summary>
    public class AlipayIserviceCcmSwArticleBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 文章信息
        /// </summary>
        [JsonPropertyName("articles")]
        public List<ArticleInfo> Articles { get; set; }
    }
}
