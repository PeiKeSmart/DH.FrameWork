﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCrowdDataSyncResponse.
    /// </summary>
    public class AlipayMarketingCrowdDataSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 人群记录ID
        /// </summary>
        [JsonPropertyName("crowd_record_id")]
        public string CrowdRecordId { get; set; }
    }
}
