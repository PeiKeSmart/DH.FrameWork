﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandAssetdeliveryAssignSyncResponse.
    /// </summary>
    public class AntMerchantExpandAssetdeliveryAssignSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 配送指令反馈处理结果.
        /// </summary>
        [JsonPropertyName("delivery_results")]
        public List<AssetResult> DeliveryResults { get; set; }
    }
}
