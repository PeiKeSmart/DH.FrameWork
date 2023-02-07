﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySecurityProdSignatureTaskCancelResponse.
    /// </summary>
    public class AlipaySecurityProdSignatureTaskCancelResponse : AlipayResponse
    {
        /// <summary>
        /// 是否更新成功
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
