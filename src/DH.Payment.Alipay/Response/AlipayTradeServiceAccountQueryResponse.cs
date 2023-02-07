﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayTradeServiceAccountQueryResponse.
    /// </summary>
    public class AlipayTradeServiceAccountQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 无限付产品账户卡信息
        /// </summary>
        [JsonPropertyName("large_infinite_card_info")]
        public LargeInfiniteCardInfo LargeInfiniteCardInfo { get; set; }
    }
}
