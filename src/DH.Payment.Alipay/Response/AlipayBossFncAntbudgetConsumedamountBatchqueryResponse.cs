﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossFncAntbudgetConsumedamountBatchqueryResponse.
    /// </summary>
    public class AlipayBossFncAntbudgetConsumedamountBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 根据BizUkIds查询出的占用情况
        /// </summary>
        [JsonPropertyName("result_data")]
        public List<BizActionConsumedAmountsDTO> ResultData { get; set; }

        /// <summary>
        /// 错误具体信息描述
        /// </summary>
        [JsonPropertyName("result_msg")]
        public string ResultMsg { get; set; }
    }
}
