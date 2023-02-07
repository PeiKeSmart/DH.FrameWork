using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppInvoiceExpensecomsueOutsourceNotifyResponse.
    /// </summary>
    public class AlipayEbppInvoiceExpensecomsueOutsourceNotifyResponse : AlipayResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
