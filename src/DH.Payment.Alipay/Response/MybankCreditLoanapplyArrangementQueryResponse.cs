﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// MybankCreditLoanapplyArrangementQueryResponse.
    /// </summary>
    public class MybankCreditLoanapplyArrangementQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 查询指定客户、产品下客户签订的合约状态等信息
        /// </summary>
        [JsonPropertyName("arrangement_query_result")]
        public List<ArrangementOpenQueryResultVO> ArrangementQueryResult { get; set; }
    }
}
