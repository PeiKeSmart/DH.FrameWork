﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMerchantOrderSecuritydetailConsumerQueryResponse.
    /// </summary>
    public class AlipayMerchantOrderSecuritydetailConsumerQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 订单信息  若订单号或业务号存在，则返回
        /// </summary>
        [JsonPropertyName("order_info")]
        public AlipayOrderDataOpenapiResultInfo OrderInfo { get; set; }
    }
}
