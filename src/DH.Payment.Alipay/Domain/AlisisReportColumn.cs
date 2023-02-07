using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlisisReportColumn Data Structure.
    /// </summary>
    public class AlisisReportColumn : AlipayObject
    {
        /// <summary>
        /// 列别名
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// 列值
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}
