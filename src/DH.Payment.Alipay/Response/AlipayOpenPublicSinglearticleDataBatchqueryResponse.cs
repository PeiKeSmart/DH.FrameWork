﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicSinglearticleDataBatchqueryResponse.
    /// </summary>
    public class AlipayOpenPublicSinglearticleDataBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 单篇文章分析数据列表
        /// </summary>
        [JsonPropertyName("data_list")]
        public List<SingleArticleAnalysisData> DataList { get; set; }
    }
}
