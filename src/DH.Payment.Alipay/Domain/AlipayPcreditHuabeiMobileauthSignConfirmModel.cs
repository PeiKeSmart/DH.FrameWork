﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayPcreditHuabeiMobileauthSignConfirmModel Data Structure.
    /// </summary>
    public class AlipayPcreditHuabeiMobileauthSignConfirmModel : AlipayObject
    {
        /// <summary>
        /// 请求参数
        /// </summary>
        [JsonPropertyName("request")]
        public string Request { get; set; }
    }
}
