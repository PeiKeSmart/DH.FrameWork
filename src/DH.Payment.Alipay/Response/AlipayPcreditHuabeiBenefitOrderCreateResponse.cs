using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayPcreditHuabeiBenefitOrderCreateResponse.
    /// </summary>
    public class AlipayPcreditHuabeiBenefitOrderCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 权益订单id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
    }
}
