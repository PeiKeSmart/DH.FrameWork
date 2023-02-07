using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenSearchServiceorderBatchqueryResponse.
    /// </summary>
    public class AlipayOpenSearchServiceorderBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 申请单列表
        /// </summary>
        [JsonPropertyName("data")]
        public OrderPageQueryDTO Data { get; set; }
    }
}
