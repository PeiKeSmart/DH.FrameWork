﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayPcreditHuabeiMobileauthSignApplyModel Data Structure.
    /// </summary>
    public class AlipayPcreditHuabeiMobileauthSignApplyModel : AlipayObject
    {
        /// <summary>
        /// 请求参数
        /// </summary>
        [JsonPropertyName("request")]
        public string Request { get; set; }
    }
}
