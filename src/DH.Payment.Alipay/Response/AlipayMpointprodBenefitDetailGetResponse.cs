﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMpointprodBenefitDetailGetResponse.
    /// </summary>
    public class AlipayMpointprodBenefitDetailGetResponse : AlipayResponse
    {
        /// <summary>
        /// 权益详情列表
        /// </summary>
        [JsonPropertyName("benefit_infos")]
        public List<BenefitInfo> BenefitInfos { get; set; }

        /// <summary>
        /// 响应码
        /// </summary>
        [JsonPropertyName("code")]
        public new string Code { get; set; }

        /// <summary>
        /// 响应描述
        /// </summary>
        [JsonPropertyName("msg")]
        public new string Msg { get; set; }
    }
}
