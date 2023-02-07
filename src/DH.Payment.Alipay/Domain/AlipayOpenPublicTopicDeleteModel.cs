using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenPublicTopicDeleteModel Data Structure.
    /// </summary>
    public class AlipayOpenPublicTopicDeleteModel : AlipayObject
    {
        /// <summary>
        /// 营销位id
        /// </summary>
        [JsonPropertyName("topic_id")]
        public string TopicId { get; set; }
    }
}
