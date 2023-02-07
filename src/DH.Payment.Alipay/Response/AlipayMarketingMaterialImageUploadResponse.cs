﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingMaterialImageUploadResponse.
    /// </summary>
    public class AlipayMarketingMaterialImageUploadResponse : AlipayResponse
    {
        /// <summary>
        /// 图片唯一资源ID
        /// </summary>
        [JsonPropertyName("resource_id")]
        public string ResourceId { get; set; }
    }
}
