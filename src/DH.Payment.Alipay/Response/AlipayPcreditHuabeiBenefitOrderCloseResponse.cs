﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayPcreditHuabeiBenefitOrderCloseResponse.
    /// </summary>
    public class AlipayPcreditHuabeiBenefitOrderCloseResponse : AlipayResponse
    {
        /// <summary>
        /// 权益订单id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// 商户侧唯一请求流水号
        /// </summary>
        [JsonPropertyName("out_request_no")]
        public string OutRequestNo { get; set; }
    }
}
