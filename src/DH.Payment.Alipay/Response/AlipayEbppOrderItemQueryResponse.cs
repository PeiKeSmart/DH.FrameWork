using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppOrderItemQueryResponse.
    /// </summary>
    public class AlipayEbppOrderItemQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 订单项
        /// </summary>
        [JsonPropertyName("item")]
        public EbppOrderItem Item { get; set; }
    }
}
