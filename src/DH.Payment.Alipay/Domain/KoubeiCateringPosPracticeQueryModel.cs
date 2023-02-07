﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiCateringPosPracticeQueryModel Data Structure.
    /// </summary>
    public class KoubeiCateringPosPracticeQueryModel : AlipayObject
    {
        /// <summary>
        /// 门店id
        /// </summary>
        [JsonPropertyName("shop_id")]
        public string ShopId { get; set; }
    }
}
