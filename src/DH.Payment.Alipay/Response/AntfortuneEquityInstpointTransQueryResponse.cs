﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntfortuneEquityInstpointTransQueryResponse.
    /// </summary>
    public class AntfortuneEquityInstpointTransQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 查询到的结果
        /// </summary>
        [JsonPropertyName("trans_info")]
        public List<PointTransInfo> TransInfo { get; set; }
    }
}
