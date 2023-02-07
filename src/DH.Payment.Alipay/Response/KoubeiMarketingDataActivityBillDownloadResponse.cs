﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingDataActivityBillDownloadResponse.
    /// </summary>
    public class KoubeiMarketingDataActivityBillDownloadResponse : AlipayResponse
    {
        /// <summary>
        /// 账单下载地址
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
