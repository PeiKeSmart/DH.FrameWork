﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayInsMarketingSellerSignResponse.
    /// </summary>
    public class AlipayInsMarketingSellerSignResponse : AlipayResponse
    {
        /// <summary>
        /// 协议号
        /// </summary>
        [JsonPropertyName("agreement_no")]
        public string AgreementNo { get; set; }
    }
}
