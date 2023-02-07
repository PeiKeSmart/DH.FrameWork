﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// MybankPaymentTradeQrcodeDeleteResponse.
    /// </summary>
    public class MybankPaymentTradeQrcodeDeleteResponse : AlipayResponse
    {
        /// <summary>
        /// true:失效成功 false:失效失败
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
