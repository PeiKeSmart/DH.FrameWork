using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenDataItemRecommendBatchqueryResponse.
    /// </summary>
    public class AlipayOpenDataItemRecommendBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 推荐结果
        /// </summary>
        [JsonPropertyName("result_obj")]
        public List<RecResultInfo> ResultObj { get; set; }
    }
}
