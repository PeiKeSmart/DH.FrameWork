using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// TagInfoVO Data Structure.
    /// </summary>
    public class TagInfoVO : AlipayObject
    {
        /// <summary>
        /// 标签code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
