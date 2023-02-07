﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossFncSettleSettlementbillCreateResponse.
    /// </summary>
    public class AlipayBossFncSettleSettlementbillCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 返回参数
        /// </summary>
        [JsonPropertyName("result_set")]
        public SettlementbillOpenApiDTO ResultSet { get; set; }
    }
}
