﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiRetailExtitemShopextitemQueryResponse.
    /// </summary>
    public class KoubeiRetailExtitemShopextitemQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 标品详情列表
        /// </summary>
        [JsonPropertyName("extitem_detail_list")]
        public List<ExtitemDetailInfo> ExtitemDetailList { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        [JsonPropertyName("total_count")]
        public long TotalCount { get; set; }
    }
}
