﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateCreditbankTrainingQueryResponse.
    /// </summary>
    public class AlipayCommerceEducateCreditbankTrainingQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 培训记录
        /// </summary>
        [JsonPropertyName("training")]
        public List<CreditBankTraining> Training { get; set; }
    }
}
