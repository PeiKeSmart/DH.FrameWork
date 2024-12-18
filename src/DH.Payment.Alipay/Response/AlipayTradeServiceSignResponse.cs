﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayTradeServiceSignResponse.
    /// </summary>
    public class AlipayTradeServiceSignResponse : AlipayResponse
    {
        /// <summary>
        /// 无限付产品的转入卡信息，服务类型为LARGE_INFINITE时返回。
        /// </summary>
        [JsonPropertyName("large_infinite_card_info")]
        public LargeInfiniteCardInfo LargeInfiniteCardInfo { get; set; }
    }
}
