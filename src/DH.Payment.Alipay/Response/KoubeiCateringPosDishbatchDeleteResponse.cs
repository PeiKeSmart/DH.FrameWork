﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringPosDishbatchDeleteResponse.
    /// </summary>
    public class KoubeiCateringPosDishbatchDeleteResponse : AlipayResponse
    {
        /// <summary>
        /// 删除成功的ID
        /// </summary>
        [JsonPropertyName("dish_ids")]
        public List<string> DishIds { get; set; }
    }
}
