﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringPosPaymodeCreateResponse.
    /// </summary>
    public class KoubeiCateringPosPaymodeCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 流水id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// 业务结果码
        /// </summary>
        [JsonPropertyName("result_code")]
        public string ResultCode { get; set; }

        /// <summary>
        /// 门店id
        /// </summary>
        [JsonPropertyName("shop_id")]
        public string ShopId { get; set; }
    }
}
