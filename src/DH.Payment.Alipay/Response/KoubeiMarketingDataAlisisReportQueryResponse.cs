﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingDataAlisisReportQueryResponse.
    /// </summary>
    public class KoubeiMarketingDataAlisisReportQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 报表数据
        /// </summary>
        [JsonPropertyName("report_data")]
        public List<AlisisReportRow> ReportData { get; set; }
    }
}
