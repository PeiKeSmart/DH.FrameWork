﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenIotbpaasMerchantorderQueryResponse.
    /// </summary>
    public class AlipayOpenIotbpaasMerchantorderQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 订单列表
        /// </summary>
        [JsonPropertyName("order_list")]
        public List<IoTBPaaSMerchantOrderInfo> OrderList { get; set; }
    }
}
