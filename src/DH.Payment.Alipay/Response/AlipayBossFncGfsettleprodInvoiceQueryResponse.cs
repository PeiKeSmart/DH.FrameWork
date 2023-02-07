using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossFncGfsettleprodInvoiceQueryResponse.
    /// </summary>
    public class AlipayBossFncGfsettleprodInvoiceQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 发票结果
        /// </summary>
        [JsonPropertyName("result_set")]
        public List<InvoiceResponseDTO> ResultSet { get; set; }
    }
}
