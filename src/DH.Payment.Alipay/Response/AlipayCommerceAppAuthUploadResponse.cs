﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceAppAuthUploadResponse.
    /// </summary>
    public class AlipayCommerceAppAuthUploadResponse : AlipayResponse
    {
        /// <summary>
        /// 业务处理结果码
        /// </summary>
        [JsonPropertyName("biz_code")]
        public string BizCode { get; set; }

        /// <summary>
        /// 业务处理结果描述
        /// </summary>
        [JsonPropertyName("biz_msg")]
        public string BizMsg { get; set; }

        /// <summary>
        /// 返回主体
        /// </summary>
        [JsonPropertyName("data")]
        public CommerceAppUploadResponseData Data { get; set; }
    }
}
