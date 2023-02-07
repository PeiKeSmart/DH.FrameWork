using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// ServiceModelContext Data Structure.
    /// </summary>
    public class ServiceModelContext : AlipayObject
    {
        /// <summary>
        /// xpId，服务调用唯一的设备标识
        /// </summary>
        [JsonPropertyName("xp_id")]
        public string XpId { get; set; }
    }
}
