﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// ZolozAuthenticationCustomerFaceabilityIdentifyResponse.
    /// </summary>
    public class ZolozAuthenticationCustomerFaceabilityIdentifyResponse : AlipayResponse
    {
        /// <summary>
        /// 能力接口返回值
        /// </summary>
        [JsonPropertyName("biz_info")]
        public FaceAbilityExtInfo BizInfo { get; set; }
    }
}
