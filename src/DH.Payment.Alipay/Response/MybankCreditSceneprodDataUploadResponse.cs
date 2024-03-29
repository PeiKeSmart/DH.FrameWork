﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// MybankCreditSceneprodDataUploadResponse.
    /// </summary>
    public class MybankCreditSceneprodDataUploadResponse : AlipayResponse
    {
        /// <summary>
        /// 网商traceId，便于查询日志内容
        /// </summary>
        [JsonPropertyName("trace_id")]
        public string TraceId { get; set; }
    }
}
