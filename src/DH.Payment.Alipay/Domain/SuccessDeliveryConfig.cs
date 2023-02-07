﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// SuccessDeliveryConfig Data Structure.
    /// </summary>
    public class SuccessDeliveryConfig : AlipayObject
    {
        /// <summary>
        /// 投放配置。
        /// </summary>
        [JsonPropertyName("delivery_config")]
        public DeliveryConfig DeliveryConfig { get; set; }
    }
}
