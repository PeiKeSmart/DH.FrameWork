﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceIotAdvertiserAdQueryModel Data Structure.
    /// </summary>
    public class AlipayCommerceIotAdvertiserAdQueryModel : AlipayObject
    {
        /// <summary>
        /// 投放计划id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
