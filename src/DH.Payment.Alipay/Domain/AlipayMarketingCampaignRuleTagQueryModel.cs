using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingCampaignRuleTagQueryModel Data Structure.
    /// </summary>
    public class AlipayMarketingCampaignRuleTagQueryModel : AlipayObject
    {
        /// <summary>
        /// 签约商户下属机构唯一编号
        /// </summary>
        [JsonPropertyName("mpid")]
        public string Mpid { get; set; }
    }
}
