﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOverseasOpenAccountConsultResponse.
    /// </summary>
    public class AlipayOverseasOpenAccountConsultResponse : AlipayResponse
    {
        /// <summary>
        /// 账号
        /// </summary>
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// isv请求结果
        /// </summary>
        [JsonPropertyName("result")]
        public TuitionISVResult Result { get; set; }
    }
}
