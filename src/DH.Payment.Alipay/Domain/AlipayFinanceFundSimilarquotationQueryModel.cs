﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayFinanceFundSimilarquotationQueryModel Data Structure.
    /// </summary>
    public class AlipayFinanceFundSimilarquotationQueryModel : AlipayObject
    {
        /// <summary>
        /// 基金代码
        /// </summary>
        [JsonPropertyName("fund_code")]
        public string FundCode { get; set; }
    }
}
