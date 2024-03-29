﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiAdvertCommissionAdvertPurchaseResponse.
    /// </summary>
    public class KoubeiAdvertCommissionAdvertPurchaseResponse : AlipayResponse
    {
        /// <summary>
        /// 主键Code结果，采购请求是批量形式的，每个identify，对应一个处理结果
        /// </summary>
        [JsonPropertyName("identify_codes")]
        public List<KbAdvertIdentifyResponse> IdentifyCodes { get; set; }
    }
}
