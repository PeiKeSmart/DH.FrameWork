using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppInvoiceInfoGetResponse.
    /// </summary>
    public class AlipayEbppInvoiceInfoGetResponse : AlipayResponse
    {
        /// <summary>
        /// 电子发票详情模型
        /// </summary>
        [JsonPropertyName("invoice_model")]
        public InvoiceModelContent InvoiceModel { get; set; }
    }
}
