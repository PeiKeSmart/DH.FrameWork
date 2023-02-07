﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySocialGiftOrderConfirmResponse.
    /// </summary>
    public class AlipaySocialGiftOrderConfirmResponse : AlipayResponse
    {
        /// <summary>
        /// 本次操作的订单id，与该接口入参order_id一致
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
    }
}
