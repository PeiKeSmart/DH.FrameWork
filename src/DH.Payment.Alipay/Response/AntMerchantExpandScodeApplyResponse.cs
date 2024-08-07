﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandScodeApplyResponse.
    /// </summary>
    public class AntMerchantExpandScodeApplyResponse : AlipayResponse
    {
        /// <summary>
        /// apply_code_response, S码申请的结果对象
        /// </summary>
        [JsonPropertyName("apply_code_response")]
        public ApplyCodeResponse ApplyCodeResponse { get; set; }
    }
}
