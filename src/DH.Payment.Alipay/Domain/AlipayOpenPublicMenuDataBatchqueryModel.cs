﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenPublicMenuDataBatchqueryModel Data Structure.
    /// </summary>
    public class AlipayOpenPublicMenuDataBatchqueryModel : AlipayObject
    {
        /// <summary>
        /// 数据开始日期，时间格式为 "yyyyMMdd" 。
        /// </summary>
        [JsonPropertyName("begin_date")]
        public string BeginDate { get; set; }

        /// <summary>
        /// 数据结束日期，时间格式为"yyyyMMdd"。查询数据开始日期/结束日期时间跨度最大30天。
        /// </summary>
        [JsonPropertyName("end_date")]
        public string EndDate { get; set; }
    }
}
