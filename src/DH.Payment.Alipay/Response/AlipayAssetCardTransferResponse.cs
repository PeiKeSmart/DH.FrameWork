﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayAssetCardTransferResponse.
    /// </summary>
    public class AlipayAssetCardTransferResponse : AlipayResponse
    {
        /// <summary>
        /// 支付宝订单id
        /// </summary>
        [JsonPropertyName("asset_order_id")]
        public string AssetOrderId { get; set; }
    }
}
