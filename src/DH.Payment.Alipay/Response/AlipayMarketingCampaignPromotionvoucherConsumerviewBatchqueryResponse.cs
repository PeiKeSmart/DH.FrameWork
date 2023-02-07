using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCampaignPromotionvoucherConsumerviewBatchqueryResponse.
    /// </summary>
    public class AlipayMarketingCampaignPromotionvoucherConsumerviewBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 券详情列表
        /// </summary>
        [JsonPropertyName("vouchers")]
        public List<VoucherDetailVO> Vouchers { get; set; }
    }
}
