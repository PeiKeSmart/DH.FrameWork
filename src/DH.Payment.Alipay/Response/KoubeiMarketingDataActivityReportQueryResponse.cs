﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingDataActivityReportQueryResponse.
    /// </summary>
    public class KoubeiMarketingDataActivityReportQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 报表
        /// </summary>
        [JsonPropertyName("report_data")]
        public string ReportData { get; set; }
    }
}
