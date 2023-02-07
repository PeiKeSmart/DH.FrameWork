﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AddTagResponse Data Structure.
    /// </summary>
    public class AddTagResponse : AlipayObject
    {
        /// <summary>
        /// 批量打标接口返回值
        /// </summary>
        [JsonPropertyName("add_tag_result")]
        public List<AddTagResult> AddTagResult { get; set; }
    }
}
