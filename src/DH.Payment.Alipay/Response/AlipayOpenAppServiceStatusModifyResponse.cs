﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenAppServiceStatusModifyResponse.
    /// </summary>
    public class AlipayOpenAppServiceStatusModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 服务编码
        /// </summary>
        [JsonPropertyName("service_code")]
        public string ServiceCode { get; set; }
    }
}
