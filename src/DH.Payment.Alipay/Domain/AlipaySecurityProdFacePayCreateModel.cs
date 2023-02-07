﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipaySecurityProdFacePayCreateModel Data Structure.
    /// </summary>
    public class AlipaySecurityProdFacePayCreateModel : AlipayObject
    {
        /// <summary>
        /// 1
        /// </summary>
        [JsonPropertyName("aaa")]
        public string Aaa { get; set; }

        /// <summary>
        /// 2
        /// </summary>
        [JsonPropertyName("aaaaaaaaaaaaa")]
        public string Aaaaaaaaaaaaa { get; set; }

        /// <summary>
        /// 1
        /// </summary>
        [JsonPropertyName("bbbbb")]
        public string Bbbbb { get; set; }
    }
}
