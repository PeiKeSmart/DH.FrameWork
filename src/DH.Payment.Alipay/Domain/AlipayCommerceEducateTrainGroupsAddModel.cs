﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceEducateTrainGroupsAddModel Data Structure.
    /// </summary>
    public class AlipayCommerceEducateTrainGroupsAddModel : AlipayObject
    {
        /// <summary>
        /// 机构群id
        /// </summary>
        [JsonPropertyName("group_ids")]
        public List<string> GroupIds { get; set; }

        /// <summary>
        /// 机构id
        /// </summary>
        [JsonPropertyName("org_id")]
        public string OrgId { get; set; }
    }
}
