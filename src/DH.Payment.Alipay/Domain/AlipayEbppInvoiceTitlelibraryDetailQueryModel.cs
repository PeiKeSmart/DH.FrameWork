using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEbppInvoiceTitlelibraryDetailQueryModel Data Structure.
    /// </summary>
    public class AlipayEbppInvoiceTitlelibraryDetailQueryModel : AlipayObject
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        [JsonPropertyName("title_name")]
        public string TitleName { get; set; }
    }
}
