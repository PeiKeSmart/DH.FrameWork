﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCardBenefitModifyResponse.
    /// </summary>
    public class AlipayMarketingCardBenefitModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 权益修改结果；true成功：false失败
        /// </summary>
        [JsonPropertyName("result")]
        public bool Result { get; set; }
    }
}
