﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMerchantPayforprivilegeCardbinactivityQueryResponse.
    /// </summary>
    public class AlipayMerchantPayforprivilegeCardbinactivityQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 查询到的卡bin活动信息
        /// </summary>
        [JsonPropertyName("card_bin_info")]
        public CardBinActivityInfo CardBinInfo { get; set; }
    }
}
