﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayDataDataserviceAdUserbalanceOnlineModel Data Structure.
    /// </summary>
    public class AlipayDataDataserviceAdUserbalanceOnlineModel : AlipayObject
    {
        /// <summary>
        /// 灯火平台提供给外部系统的访问token
        /// </summary>
        [JsonPropertyName("biz_token")]
        public string BizToken { get; set; }

        /// <summary>
        /// 投放账户id列表
        /// </summary>
        [JsonPropertyName("user_id_list")]
        public List<long> UserIdList { get; set; }
    }
}
