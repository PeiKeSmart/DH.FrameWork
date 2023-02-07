﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataIotdataTextdifferenceBaiQueryResponse.
    /// </summary>
    public class AlipayDataIotdataTextdifferenceBaiQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 差异化结果以json字符串呈现。
        /// </summary>
        [JsonPropertyName("data")]
        public List<TextDiff> Data { get; set; }
    }
}
