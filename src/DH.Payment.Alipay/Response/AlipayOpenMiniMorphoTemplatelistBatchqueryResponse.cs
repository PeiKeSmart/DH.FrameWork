﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniMorphoTemplatelistBatchqueryResponse.
    /// </summary>
    public class AlipayOpenMiniMorphoTemplatelistBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 模板列表
        /// </summary>
        [JsonPropertyName("data_list")]
        public List<MorphoTemplateItem> DataList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        [JsonPropertyName("paginator")]
        public MorphoPaginator Paginator { get; set; }
    }
}
