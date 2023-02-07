﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiRetailWmsPartnerQueryResponse.
    /// </summary>
    public class KoubeiRetailWmsPartnerQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 商户档案信息
        /// </summary>
        [JsonPropertyName("partners")]
        public List<PartnerVO> Partners { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        [JsonPropertyName("total_count")]
        public string TotalCount { get; set; }
    }
}
