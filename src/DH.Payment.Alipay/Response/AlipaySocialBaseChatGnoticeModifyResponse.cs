﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySocialBaseChatGnoticeModifyResponse.
    /// </summary>
    public class AlipaySocialBaseChatGnoticeModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 修改结果
        /// </summary>
        [JsonPropertyName("result_modify")]
        public string ResultModify { get; set; }
    }
}
