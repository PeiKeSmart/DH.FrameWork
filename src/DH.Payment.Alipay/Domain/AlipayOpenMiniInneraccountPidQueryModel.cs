﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenMiniInneraccountPidQueryModel Data Structure.
    /// </summary>
    public class AlipayOpenMiniInneraccountPidQueryModel : AlipayObject
    {
        /// <summary>
        /// 业务类型
        /// </summary>
        [JsonPropertyName("client_type")]
        public string ClientType { get; set; }

        /// <summary>
        /// 外部业务主体ID
        /// </summary>
        [JsonPropertyName("out_biz_id")]
        public string OutBizId { get; set; }
    }
}
