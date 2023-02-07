using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// ConsultParams Data Structure.
    /// </summary>
    public class ConsultParams : AlipayObject
    {
        /// <summary>
        /// 集团havana ID
        /// </summary>
        [JsonPropertyName("another_hid")]
        public string AnotherHid { get; set; }
    }
}
