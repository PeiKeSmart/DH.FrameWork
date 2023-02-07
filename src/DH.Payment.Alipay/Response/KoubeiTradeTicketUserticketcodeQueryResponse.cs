using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiTradeTicketUserticketcodeQueryResponse.
    /// </summary>
    public class KoubeiTradeTicketUserticketcodeQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 查询结果列表
        /// </summary>
        [JsonPropertyName("values")]
        public List<TicketCodeQueryResponse> Values { get; set; }
    }
}
