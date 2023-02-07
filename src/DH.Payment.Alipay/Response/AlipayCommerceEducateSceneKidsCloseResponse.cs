﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateSceneKidsCloseResponse.
    /// </summary>
    public class AlipayCommerceEducateSceneKidsCloseResponse : AlipayResponse
    {
        /// <summary>
        /// 关闭业务是否成功
        /// </summary>
        [JsonPropertyName("close_success")]
        public string CloseSuccess { get; set; }
    }
}
