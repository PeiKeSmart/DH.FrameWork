﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiMarketingCampaignMemberTemplateOnlineModel Data Structure.
    /// </summary>
    public class KoubeiMarketingCampaignMemberTemplateOnlineModel : AlipayObject
    {
        /// <summary>
        /// 会员模板id
        /// </summary>
        [JsonPropertyName("member_template_id")]
        public string MemberTemplateId { get; set; }

        /// <summary>
        /// 请求ID，由开发者生成并保证唯一性
        /// </summary>
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }
    }
}
