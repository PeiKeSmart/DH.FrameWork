﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiSalesKbassetStuffProduceorderBatchqueryModel Data Structure.
    /// </summary>
    public class KoubeiSalesKbassetStuffProduceorderBatchqueryModel : AlipayObject
    {
        /// <summary>
        /// 每页容量：最小1，最大100
        /// </summary>
        [JsonPropertyName("page_size")]
        public string PageSize { get; set; }
    }
}
