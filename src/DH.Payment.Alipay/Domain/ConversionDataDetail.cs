﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// ConversionDataDetail Data Structure.
    /// </summary>
    public class ConversionDataDetail : AlipayObject
    {
        /// <summary>
        /// 转化事件id
        /// </summary>
        [JsonPropertyName("conversion_id")]
        public string ConversionId { get; set; }

        /// <summary>
        /// 转化事件结果
        /// </summary>
        [JsonPropertyName("conversion_result")]
        public string ConversionResult { get; set; }
    }
}
