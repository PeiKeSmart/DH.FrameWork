using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceIotSdarttoolMessageQueryModel Data Structure.
    /// </summary>
    public class AlipayCommerceIotSdarttoolMessageQueryModel : AlipayObject
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        [JsonPropertyName("message_no")]
        public string MessageNo { get; set; }
    }
}
