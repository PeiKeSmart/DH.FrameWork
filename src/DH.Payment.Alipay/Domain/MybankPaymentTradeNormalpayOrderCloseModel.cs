using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// MybankPaymentTradeNormalpayOrderCloseModel Data Structure.
    /// </summary>
    public class MybankPaymentTradeNormalpayOrderCloseModel : AlipayObject
    {
        /// <summary>
        /// 网商订单号
        /// </summary>
        [JsonPropertyName("order_no")]
        public string OrderNo { get; set; }
    }
}
