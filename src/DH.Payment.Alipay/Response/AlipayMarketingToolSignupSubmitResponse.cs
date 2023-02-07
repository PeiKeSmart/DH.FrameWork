﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolSignupSubmitResponse.
    /// </summary>
    public class AlipayMarketingToolSignupSubmitResponse : AlipayResponse
    {
        /// <summary>
        /// 玩法ID。
        /// </summary>
        [JsonPropertyName("play_id")]
        public string PlayId { get; set; }
    }
}
