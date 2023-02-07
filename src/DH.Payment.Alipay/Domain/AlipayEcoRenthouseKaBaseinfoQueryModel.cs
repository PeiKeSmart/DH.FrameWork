﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEcoRenthouseKaBaseinfoQueryModel Data Structure.
    /// </summary>
    public class AlipayEcoRenthouseKaBaseinfoQueryModel : AlipayObject
    {
        /// <summary>
        /// kaCode唯一标识
        /// </summary>
        [JsonPropertyName("ka_code")]
        public string KaCode { get; set; }
    }
}
