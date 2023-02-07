﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// ZhimaCreditPeZmgoPreconsultQueryResponse.
    /// </summary>
    public class ZhimaCreditPeZmgoPreconsultQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 是否准入
        /// </summary>
        [JsonPropertyName("admittance")]
        public bool Admittance { get; set; }
    }
}
