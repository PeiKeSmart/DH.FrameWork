﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniIsvCreateResponse.
    /// </summary>
    public class AlipayOpenMiniIsvCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 小程序代创建订单号
        /// </summary>
        [JsonPropertyName("order_no")]
        public string OrderNo { get; set; }
    }
}
