﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicUserDataBatchqueryResponse.
    /// </summary>
    public class AlipayOpenPublicUserDataBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 用户分析数据
        /// </summary>
        [JsonPropertyName("data_list")]
        public List<UserAnalysisData> DataList { get; set; }
    }
}
