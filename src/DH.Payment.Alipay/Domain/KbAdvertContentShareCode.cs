﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// KbAdvertContentShareCode Data Structure.
    /// </summary>
    public class KbAdvertContentShareCode : AlipayObject
    {
        /// <summary>
        /// 吱口令内容详情
        /// </summary>
        [JsonPropertyName("share_code_desc")]
        public string ShareCodeDesc { get; set; }
    }
}
