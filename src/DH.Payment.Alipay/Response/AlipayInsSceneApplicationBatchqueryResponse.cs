using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayInsSceneApplicationBatchqueryResponse.
    /// </summary>
    public class AlipayInsSceneApplicationBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 投保单列表
        /// </summary>
        [JsonPropertyName("applications")]
        public List<InsApplicationQuery> Applications { get; set; }
    }
}
