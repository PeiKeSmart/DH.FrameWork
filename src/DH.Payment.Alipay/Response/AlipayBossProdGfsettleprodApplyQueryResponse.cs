﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossProdGfsettleprodApplyQueryResponse.
    /// </summary>
    public class AlipayBossProdGfsettleprodApplyQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 付款（开票）申请
        /// </summary>
        [JsonPropertyName("result_set")]
        public List<PaymentApplyResponseDTO> ResultSet { get; set; }
    }
}
