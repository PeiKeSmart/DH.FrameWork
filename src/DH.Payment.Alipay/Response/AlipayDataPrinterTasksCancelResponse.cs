﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataPrinterTasksCancelResponse.
    /// </summary>
    public class AlipayDataPrinterTasksCancelResponse : AlipayResponse
    {
        /// <summary>
        /// 状态码
        /// </summary>
        [JsonPropertyName("code")]
        public new string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
