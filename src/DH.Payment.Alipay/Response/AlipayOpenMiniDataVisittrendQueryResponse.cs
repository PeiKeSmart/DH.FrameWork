﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniDataVisittrendQueryResponse.
    /// </summary>
    public class AlipayOpenMiniDataVisittrendQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 小程序实时访问趋势结果
        /// </summary>
        [JsonPropertyName("app_visit_trend_data_list_response")]
        public List<AppVisitTrendDataResponse> AppVisitTrendDataListResponse { get; set; }
    }
}
