﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenAppCommunityPartnerSyncResponse.
    /// </summary>
    public class AlipayOpenAppCommunityPartnerSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 目标对象数据
        /// </summary>
        [JsonPropertyName("target_list")]
        public List<CommunityPartnerRelationDataSyncDTO> TargetList { get; set; }
    }
}
