﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipaySecurityRiskBackgroundQueryModel Data Structure.
    /// </summary>
    public class AlipaySecurityRiskBackgroundQueryModel : AlipayObject
    {
        /// <summary>
        /// params+用于背调查询的输入信息+用户传入
        /// </summary>
        [JsonPropertyName("params")]
        public string Params { get; set; }

        /// <summary>
        /// partner_name+唯一+作为标识调用者身份的字段+用户填入
        /// </summary>
        [JsonPropertyName("partner_name")]
        public string PartnerName { get; set; }
    }
}
