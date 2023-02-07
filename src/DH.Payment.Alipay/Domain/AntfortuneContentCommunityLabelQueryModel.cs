using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AntfortuneContentCommunityLabelQueryModel Data Structure.
    /// </summary>
    public class AntfortuneContentCommunityLabelQueryModel : AlipayObject
    {
        /// <summary>
        /// 标签场景
        /// </summary>
        [JsonPropertyName("label_scene")]
        public string LabelScene { get; set; }
    }
}
