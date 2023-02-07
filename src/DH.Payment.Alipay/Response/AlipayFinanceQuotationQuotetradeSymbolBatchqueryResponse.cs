﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayFinanceQuotationQuotetradeSymbolBatchqueryResponse.
    /// </summary>
    public class AlipayFinanceQuotationQuotetradeSymbolBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 码表
        /// </summary>
        [JsonPropertyName("data")]
        public List<SymbolDTO> Data { get; set; }
    }
}
