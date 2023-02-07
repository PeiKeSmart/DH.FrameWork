using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiRetailWmsProducerBatchqueryResponse.
    /// </summary>
    public class KoubeiRetailWmsProducerBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 生产厂商信息
        /// </summary>
        [JsonPropertyName("producers")]
        public List<ProducerVO> Producers { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        [JsonPropertyName("total_count")]
        public long TotalCount { get; set; }
    }
}
