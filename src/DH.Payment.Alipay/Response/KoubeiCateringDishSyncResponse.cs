﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringDishSyncResponse.
    /// </summary>
    public class KoubeiCateringDishSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 返回的菜品模型
        /// </summary>
        [JsonPropertyName("kb_dish_info")]
        public KbdishInfo KbDishInfo { get; set; }
    }
}
