﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdieSpaceCreateResponse.
    /// </summary>
    public class AlipayMarketingToolFengdieSpaceCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 创建成功后返回空间的基本信息，包含空间 ID、标题、创建时间与可用域名列表
        /// </summary>
        [JsonPropertyName("data")]
        public FengdieSpaceDetailModel Data { get; set; }
    }
}
