﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEcoEntertainmentOrderUploadModel Data Structure.
    /// </summary>
    public class AlipayEcoEntertainmentOrderUploadModel : AlipayObject
    {
        /// <summary>
        /// 数娱充值ISV订单回流模型
        /// </summary>
        [JsonPropertyName("entertainment_order_info")]
        public EntertainmentOrderInfo EntertainmentOrderInfo { get; set; }
    }
}
