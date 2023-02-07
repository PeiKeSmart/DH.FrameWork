using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignMemberTemplateBatchqueryResponse.
    /// </summary>
    public class KoubeiMarketingCampaignMemberTemplateBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 会员模板模型
        /// </summary>
        [JsonPropertyName("member_templates")]
        public List<MerchantMemberTemplateModel> MemberTemplates { get; set; }
    }
}
