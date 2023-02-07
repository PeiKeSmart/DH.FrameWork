using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceIotVoicemodelCustomvoiceSendModel Data Structure.
    /// </summary>
    public class AlipayCommerceIotVoicemodelCustomvoiceSendModel : AlipayObject
    {
        /// <summary>
        /// 服务调用上下文
        /// </summary>
        [JsonPropertyName("context")]
        public ServiceModelContext Context { get; set; }

        /// <summary>
        /// 自定义语音调用模型
        /// </summary>
        [JsonPropertyName("custom_voice")]
        public CustomVoiceVO CustomVoice { get; set; }
    }
}
