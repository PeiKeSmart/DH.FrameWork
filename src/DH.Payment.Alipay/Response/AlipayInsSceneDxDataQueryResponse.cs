﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayInsSceneDxDataQueryResponse.
    /// </summary>
    public class AlipayInsSceneDxDataQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 返回dx数据
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}
