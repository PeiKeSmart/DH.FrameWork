using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEbppInvoiceTitlelibraryListQueryModel Data Structure.
    /// </summary>
    public class AlipayEbppInvoiceTitlelibraryListQueryModel : AlipayObject
    {
        /// <summary>
        /// 模糊查询的抬头名称
        /// </summary>
        [JsonPropertyName("title_simple_name")]
        public string TitleSimpleName { get; set; }
    }
}
