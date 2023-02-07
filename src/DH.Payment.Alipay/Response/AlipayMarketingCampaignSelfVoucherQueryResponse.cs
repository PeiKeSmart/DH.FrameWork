using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCampaignSelfVoucherQueryResponse.
    /// </summary>
    public class AlipayMarketingCampaignSelfVoucherQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 券列表
        /// </summary>
        [JsonPropertyName("voucher_item")]
        public VoucherItem VoucherItem { get; set; }
    }
}
