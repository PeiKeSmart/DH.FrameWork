﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateInfoParticipantCertifyResponse.
    /// </summary>
    public class AlipayCommerceEducateInfoParticipantCertifyResponse : AlipayResponse
    {
        /// <summary>
        /// 外部支付流水号
        /// </summary>
        [JsonPropertyName("out_payid")]
        public string OutPayid { get; set; }
    }
}
