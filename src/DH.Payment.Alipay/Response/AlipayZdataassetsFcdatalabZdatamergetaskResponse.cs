﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayZdataassetsFcdatalabZdatamergetaskResponse.
    /// </summary>
    public class AlipayZdataassetsFcdatalabZdatamergetaskResponse : AlipayResponse
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}
