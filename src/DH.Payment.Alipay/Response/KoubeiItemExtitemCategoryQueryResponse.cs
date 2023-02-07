﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiItemExtitemCategoryQueryResponse.
    /// </summary>
    public class KoubeiItemExtitemCategoryQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 品类信息列表
        /// </summary>
        [JsonPropertyName("category_list")]
        public List<ExtCategory> CategoryList { get; set; }
    }
}
