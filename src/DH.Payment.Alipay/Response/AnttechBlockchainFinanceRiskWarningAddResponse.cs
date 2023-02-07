﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AnttechBlockchainFinanceRiskWarningAddResponse.
    /// </summary>
    public class AnttechBlockchainFinanceRiskWarningAddResponse : AlipayResponse
    {
        /// <summary>
        /// 提交结果
        /// </summary>
        [JsonPropertyName("submit_result")]
        public string SubmitResult { get; set; }
    }
}
