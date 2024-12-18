﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCreditCreditriskDataPutResponse.
    /// </summary>
    public class AlipayCreditCreditriskDataPutResponse : AlipayResponse
    {
        /// <summary>
        /// 返回该条数据在网商的唯一ID
        /// </summary>
        [JsonPropertyName("dataid")]
        public string Dataid { get; set; }
    }
}
