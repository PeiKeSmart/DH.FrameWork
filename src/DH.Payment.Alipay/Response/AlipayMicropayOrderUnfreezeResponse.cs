using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMicropayOrderUnfreezeResponse.
    /// </summary>
    public class AlipayMicropayOrderUnfreezeResponse : AlipayResponse
    {
        /// <summary>
        /// 冻结订单详情结果
        /// </summary>
        [JsonPropertyName("unfreeze_order_detail")]
        public UnfreezeOrderDetail UnfreezeOrderDetail { get; set; }
    }
}
