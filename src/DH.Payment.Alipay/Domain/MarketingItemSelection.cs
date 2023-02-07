using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// MarketingItemSelection Data Structure.
    /// </summary>
    public class MarketingItemSelection : AlipayObject
    {
        /// <summary>
        /// 营销项目ID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
