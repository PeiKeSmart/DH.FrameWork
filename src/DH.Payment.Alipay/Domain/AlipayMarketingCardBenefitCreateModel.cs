﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingCardBenefitCreateModel Data Structure.
    /// </summary>
    public class AlipayMarketingCardBenefitCreateModel : AlipayObject
    {
        /// <summary>
        /// 会员卡模板外部权益
        /// </summary>
        [JsonPropertyName("mcard_template_benefit")]
        public McardTemplateBenefit McardTemplateBenefit { get; set; }
    }
}
