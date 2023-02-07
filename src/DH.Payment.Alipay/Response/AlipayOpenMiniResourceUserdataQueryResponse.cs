﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniResourceUserdataQueryResponse.
    /// </summary>
    public class AlipayOpenMiniResourceUserdataQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 流量位用户数据列表
        /// </summary>
        [JsonPropertyName("user_data_list")]
        public List<ResourceUserDataVO> UserDataList { get; set; }
    }
}
