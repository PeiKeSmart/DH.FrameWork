﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// ZhimaCreditEpSceneFulfillmentlistSyncResponse.
    /// </summary>
    public class ZhimaCreditEpSceneFulfillmentlistSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 履约同步结果列表
        /// </summary>
        [JsonPropertyName("fulfillment_result_list")]
        public List<FulfillmentResult> FulfillmentResultList { get; set; }
    }
}
