﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// BenefitSource Data Structure.
    /// </summary>
    public class BenefitSource : AlipayObject
    {
        /// <summary>
        /// 来源类型，比如CAMPAIG
        /// </summary>
        [JsonPropertyName("benefit_source_type")]
        public string BenefitSourceType { get; set; }

        /// <summary>
        /// 10000455203
        /// </summary>
        [JsonPropertyName("benefit_source_value")]
        public string BenefitSourceValue { get; set; }
    }
}
