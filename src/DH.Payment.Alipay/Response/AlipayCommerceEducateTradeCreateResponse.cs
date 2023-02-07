﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateTradeCreateResponse.
    /// </summary>
    public class AlipayCommerceEducateTradeCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [JsonPropertyName("edu_trade_no")]
        public string EduTradeNo { get; set; }
    }
}
