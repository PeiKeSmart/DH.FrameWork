using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// RtaInfo Data Structure.
    /// </summary>
    public class RtaInfo : AlipayObject
    {
        /// <summary>
        /// 广告投放账户id
        /// </summary>
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }
    }
}
