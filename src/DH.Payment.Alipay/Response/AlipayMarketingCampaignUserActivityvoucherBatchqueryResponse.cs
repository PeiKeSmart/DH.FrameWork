using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCampaignUserActivityvoucherBatchqueryResponse.
    /// </summary>
    public class AlipayMarketingCampaignUserActivityvoucherBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 活动券列表
        /// </summary>
        [JsonPropertyName("activity_voucher_list")]
        public List<ActivityVoucherInfo> ActivityVoucherList { get; set; }
    }
}
