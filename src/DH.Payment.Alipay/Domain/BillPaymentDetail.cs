using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// BillPaymentDetail Data Structure.
    /// </summary>
    public class BillPaymentDetail : AlipayObject
    {
        /// <summary>
        /// 缴费项ID
        /// </summary>
        [JsonPropertyName("bill_payment_id")]
        public string BillPaymentId { get; set; }
    }
}
