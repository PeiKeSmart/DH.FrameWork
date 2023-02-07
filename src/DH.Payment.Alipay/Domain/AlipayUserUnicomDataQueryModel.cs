﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayUserUnicomDataQueryModel Data Structure.
    /// </summary>
    public class AlipayUserUnicomDataQueryModel : AlipayObject
    {
        /// <summary>
        /// 蚂蚁统一会员ID
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}
