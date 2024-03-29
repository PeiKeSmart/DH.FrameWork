﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntProdpaasArrangementCommonQueryResponse.
    /// </summary>
    public class AntProdpaasArrangementCommonQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 合约信息标准VO，一条合约一个记录
        /// </summary>
        [JsonPropertyName("arrangements")]
        public List<ArrangementVORes> Arrangements { get; set; }
    }
}
