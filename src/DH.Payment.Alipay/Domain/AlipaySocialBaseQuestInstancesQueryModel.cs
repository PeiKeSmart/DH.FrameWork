﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipaySocialBaseQuestInstancesQueryModel Data Structure.
    /// </summary>
    public class AlipaySocialBaseQuestInstancesQueryModel : AlipayObject
    {
        /// <summary>
        /// 目标ID列表，列表长度不能超过20
        /// </summary>
        [JsonPropertyName("quest_ids")]
        public List<string> QuestIds { get; set; }

        /// <summary>
        /// 调用方标识
        /// </summary>
        [JsonPropertyName("source_id")]
        public string SourceId { get; set; }

        /// <summary>
        /// 蚂蚁统一会员ID
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}
