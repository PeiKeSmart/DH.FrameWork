﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenAppLingbalingliuQueryModel Data Structure.
    /// </summary>
    public class AlipayOpenAppLingbalingliuQueryModel : AlipayObject
    {
        /// <summary>
        /// 12
        /// </summary>
        [JsonPropertyName("canshu")]
        public string Canshu { get; set; }
    }
}
