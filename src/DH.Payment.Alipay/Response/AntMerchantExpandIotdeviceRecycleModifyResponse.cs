﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandIotdeviceRecycleModifyResponse.
    /// </summary>
    public class AntMerchantExpandIotdeviceRecycleModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 业务处理订单号
        /// </summary>
        [JsonPropertyName("biz_order_id")]
        public string BizOrderId { get; set; }
    }
}
