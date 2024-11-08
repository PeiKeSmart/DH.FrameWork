﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// ZolozIdentificationUserWebQueryResponse.
    /// </summary>
    public class ZolozIdentificationUserWebQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 扩展结果
        /// </summary>
        [JsonPropertyName("extern_info")]
        public string ExternInfo { get; set; }
    }
}
