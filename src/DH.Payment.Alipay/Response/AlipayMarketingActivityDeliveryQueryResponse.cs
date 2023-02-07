﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingActivityDeliveryQueryResponse.
    /// </summary>
    public class AlipayMarketingActivityDeliveryQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 投放信息列表
        /// </summary>
        [JsonPropertyName("delivery_info_list")]
        public List<PromoDeliveryInfo> DeliveryInfoList { get; set; }
    }
}
