using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppInvoiceApplystatusQueryResponse.
    /// </summary>
    public class AlipayEbppInvoiceApplystatusQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 每笔交易开票状态
        /// </summary>
        [JsonPropertyName("apply_status_brief_dtos")]
        public List<OrderApplyStatusBriefDTO> ApplyStatusBriefDtos { get; set; }
    }
}
