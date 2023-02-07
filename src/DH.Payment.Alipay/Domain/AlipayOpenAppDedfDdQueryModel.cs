﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenAppDedfDdQueryModel Data Structure.
    /// </summary>
    public class AlipayOpenAppDedfDdQueryModel : AlipayObject
    {
        /// <summary>
        /// 10
        /// </summary>
        [JsonPropertyName("zyde")]
        public string Zyde { get; set; }
    }
}
