﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandShopModifyResponse.
    /// </summary>
    public class AntMerchantExpandShopModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 申请单id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
    }
}
