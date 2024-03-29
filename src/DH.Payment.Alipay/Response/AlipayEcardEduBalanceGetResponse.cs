﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcardEduBalanceGetResponse.
    /// </summary>
    public class AlipayEcardEduBalanceGetResponse : AlipayResponse
    {
        /// <summary>
        /// 校园一卡通余额查询结果对象
        /// </summary>
        [JsonPropertyName("balance")]
        public List<EduOneCardBalanceQueryResult> Balance { get; set; }
    }
}
