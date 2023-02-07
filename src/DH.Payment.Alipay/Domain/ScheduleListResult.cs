﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// ScheduleListResult Data Structure.
    /// </summary>
    public class ScheduleListResult : AlipayObject
    {
        /// <summary>
        /// 返回码
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        [JsonPropertyName("data")]
        public List<ScheduleListItem> Data { get; set; }

        /// <summary>
        /// 返回码描述
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
