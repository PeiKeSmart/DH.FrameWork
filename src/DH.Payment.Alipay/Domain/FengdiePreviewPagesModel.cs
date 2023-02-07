﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// FengdiePreviewPagesModel Data Structure.
    /// </summary>
    public class FengdiePreviewPagesModel : AlipayObject
    {
        /// <summary>
        /// 站点页面别名
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// 页面预览地址
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
