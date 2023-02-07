﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceKidsAccountQueryModel Data Structure.
    /// </summary>
    public class AlipayCommerceKidsAccountQueryModel : AlipayObject
    {
        /// <summary>
        /// 登陆名
        /// </summary>
        [JsonPropertyName("login_name")]
        public string LoginName { get; set; }

        /// <summary>
        /// 产品码
        /// </summary>
        [JsonPropertyName("product_code")]
        public string ProductCode { get; set; }
    }
}
