﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayAccountExrateRatequeryResponse.
    /// </summary>
    public class AlipayAccountExrateRatequeryResponse : AlipayResponse
    {
        /// <summary>
        /// 查询到的汇率对象列表，如果没有查询到则返回空列表
        /// </summary>
        [JsonPropertyName("rate_query_response_list")]
        public List<ExRefRateInfoVO> RateQueryResponseList { get; set; }
    }
}
