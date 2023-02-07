using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayBossBaseProcessTicketQueryModel Data Structure.
    /// </summary>
    public class AlipayBossBaseProcessTicketQueryModel : AlipayObject
    {
        /// <summary>
        /// 流程实例Id
        /// </summary>
        [JsonPropertyName("puid")]
        public string Puid { get; set; }
    }
}
