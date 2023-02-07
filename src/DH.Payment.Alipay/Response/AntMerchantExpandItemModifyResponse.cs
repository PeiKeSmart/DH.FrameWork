﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandItemModifyResponse.
    /// </summary>
    public class AntMerchantExpandItemModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 商品id
        /// </summary>
        [JsonPropertyName("item_id")]
        public string ItemId { get; set; }
    }
}
