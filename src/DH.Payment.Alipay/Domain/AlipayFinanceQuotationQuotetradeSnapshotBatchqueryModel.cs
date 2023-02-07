﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayFinanceQuotationQuotetradeSnapshotBatchqueryModel Data Structure.
    /// </summary>
    public class AlipayFinanceQuotationQuotetradeSnapshotBatchqueryModel : AlipayObject
    {
        /// <summary>
        /// 股票唯一编码，symbol=code.market
        /// </summary>
        [JsonPropertyName("symbols")]
        public List<string> Symbols { get; set; }
    }
}
