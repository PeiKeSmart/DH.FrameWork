using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppInvoiceExpenserulesProjectemployeeModifyResponse.
    /// </summary>
    public class AlipayEbppInvoiceExpenserulesProjectemployeeModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
