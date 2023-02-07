using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMicropayOrderGetResponse.
    /// </summary>
    public class AlipayMicropayOrderGetResponse : AlipayResponse
    {
        /// <summary>
        /// 冻结订单详情
        /// </summary>
        [JsonPropertyName("micro_pay_order_detail")]
        public MicroPayOrderDetail MicroPayOrderDetail { get; set; }
    }
}
