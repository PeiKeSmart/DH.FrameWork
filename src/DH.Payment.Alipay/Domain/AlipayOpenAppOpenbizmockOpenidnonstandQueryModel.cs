﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenAppOpenbizmockOpenidnonstandQueryModel Data Structure.
    /// </summary>
    public class AlipayOpenAppOpenbizmockOpenidnonstandQueryModel : AlipayObject
    {
        /// <summary>
        /// 请求
        /// </summary>
        [JsonPropertyName("request")]
        public string Request { get; set; }
    }
}
