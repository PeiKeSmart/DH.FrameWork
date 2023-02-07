using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayFinanceQuotationQuotetradeSnapshotBatchqueryResponse.
    /// </summary>
    public class AlipayFinanceQuotationQuotetradeSnapshotBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 快照对象
        /// </summary>
        [JsonPropertyName("data")]
        public List<SnapshotDTO> Data { get; set; }
    }
}
