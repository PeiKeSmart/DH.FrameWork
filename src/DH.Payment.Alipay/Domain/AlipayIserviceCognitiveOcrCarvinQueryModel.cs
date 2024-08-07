﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayIserviceCognitiveOcrCarvinQueryModel Data Structure.
    /// </summary>
    public class AlipayIserviceCognitiveOcrCarvinQueryModel : AlipayObject
    {
        /// <summary>
        /// 汽车VIN 图片base64 encode内容
        /// </summary>
        [JsonPropertyName("image_content")]
        public string ImageContent { get; set; }
    }
}
