using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayAccountFinriskInstriskmonitorKeywordsBatchqueryModel Data Structure.
    /// </summary>
    public class AlipayAccountFinriskInstriskmonitorKeywordsBatchqueryModel : AlipayObject
    {
        /// <summary>
        /// 查询机构关键词
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
