﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenInstantdeliveryAccountCreateResponse.
    /// </summary>
    public class AlipayOpenInstantdeliveryAccountCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 配送公司账户创建结果列表
        /// </summary>
        [JsonPropertyName("logistics_account_status")]
        public List<LogisticsAccountStatusDTO> LogisticsAccountStatus { get; set; }
    }
}
