﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingActivityOrdervoucherCodecountModel Data Structure.
    /// </summary>
    public class AlipayMarketingActivityOrdervoucherCodecountModel : AlipayObject
    {
        /// <summary>
        /// 活动id
        /// </summary>
        [JsonPropertyName("activity_id")]
        public string ActivityId { get; set; }
    }
}
