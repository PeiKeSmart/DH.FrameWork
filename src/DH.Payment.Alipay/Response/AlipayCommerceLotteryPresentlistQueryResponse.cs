﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceLotteryPresentlistQueryResponse.
    /// </summary>
    public class AlipayCommerceLotteryPresentlistQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 列表内容
        /// </summary>
        [JsonPropertyName("results")]
        public List<LotteryPresent> Results { get; set; }

        /// <summary>
        /// 返回的列表的大小
        /// </summary>
        [JsonPropertyName("total_result")]
        public long TotalResult { get; set; }
    }
}
