﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayBossBaseInstanceOperatetraceQueryModel Data Structure.
    /// </summary>
    public class AlipayBossBaseInstanceOperatetraceQueryModel : AlipayObject
    {
        /// <summary>
        /// 流程实例全局唯一ID
        /// </summary>
        [JsonPropertyName("puid")]
        public string Puid { get; set; }
    }
}
