﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayAssetCardDisburseResponse.
    /// </summary>
    public class AlipayAssetCardDisburseResponse : AlipayResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [JsonPropertyName("order_no")]
        public string OrderNo { get; set; }
    }
}
