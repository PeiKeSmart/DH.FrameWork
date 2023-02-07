using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySocialBaseQuestInstanceAddResponse.
    /// </summary>
    public class AlipaySocialBaseQuestInstanceAddResponse : AlipayResponse
    {
        /// <summary>
        /// 小目标实例模型
        /// </summary>
        [JsonPropertyName("instance")]
        public QuestInstanceDO Instance { get; set; }
    }
}
