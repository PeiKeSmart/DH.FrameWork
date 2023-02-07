﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceIotVoicemodelSyncvoiceSendResponse.
    /// </summary>
    public class AlipayCommerceIotVoicemodelSyncvoiceSendResponse : AlipayResponse
    {
        /// <summary>
        /// 调用的任务id
        /// </summary>
        [JsonPropertyName("task_id")]
        public string TaskId { get; set; }
    }
}
