﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppCommunityExternalbillsyncUploadResponse.
    /// </summary>
    public class AlipayEbppCommunityExternalbillsyncUploadResponse : AlipayResponse
    {
        /// <summary>
        /// 失败总数
        /// </summary>
        [JsonPropertyName("fail_count")]
        public string FailCount { get; set; }

        /// <summary>
        /// 失败列表，用于告知失败账单号和原因
        /// </summary>
        [JsonPropertyName("fail_external_sync")]
        public List<FailExternalSync> FailExternalSync { get; set; }

        /// <summary>
        /// 成功总数
        /// </summary>
        [JsonPropertyName("success_count")]
        public string SuccessCount { get; set; }
    }
}
