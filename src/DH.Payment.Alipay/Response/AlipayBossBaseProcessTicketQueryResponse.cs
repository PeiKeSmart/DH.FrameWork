using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossBaseProcessTicketQueryResponse.
    /// </summary>
    public class AlipayBossBaseProcessTicketQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 工单详情
        /// </summary>
        [JsonPropertyName("ticket")]
        public BPOpenApiTicket Ticket { get; set; }
    }
}
