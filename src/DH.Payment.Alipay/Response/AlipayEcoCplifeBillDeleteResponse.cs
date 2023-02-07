﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoCplifeBillDeleteResponse.
    /// </summary>
    public class AlipayEcoCplifeBillDeleteResponse : AlipayResponse
    {
        /// <summary>
        /// 不允许删除（支付中或者支付完成）的账单明细条目列表
        /// </summary>
        [JsonPropertyName("alive_bill_entry_list")]
        public List<CPAliveBillEntrySet> AliveBillEntryList { get; set; }
    }
}
