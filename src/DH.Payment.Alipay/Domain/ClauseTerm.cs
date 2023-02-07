﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// ClauseTerm Data Structure.
    /// </summary>
    public class ClauseTerm : AlipayObject
    {
        /// <summary>
        /// 说明描述内容
        /// </summary>
        [JsonPropertyName("descriptions")]
        public List<string> Descriptions { get; set; }

        /// <summary>
        /// 说明title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
