﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySocialAntcommonwealDonateConsumeSyncResponse.
    /// </summary>
    public class AlipaySocialAntcommonwealDonateConsumeSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonPropertyName("result_code")]
        public string ResultCode { get; set; }

        /// <summary>
        /// 成功/失败
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
