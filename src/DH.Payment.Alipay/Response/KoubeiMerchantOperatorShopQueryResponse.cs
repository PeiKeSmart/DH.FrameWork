﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMerchantOperatorShopQueryResponse.
    /// </summary>
    public class KoubeiMerchantOperatorShopQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 操作员关联的门店模型列表
        /// </summary>
        [JsonPropertyName("city_shop_models")]
        public List<CityShopModel> CityShopModels { get; set; }
    }
}
