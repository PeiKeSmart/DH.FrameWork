﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenMiniInnerversionLastQueryModel Data Structure.
    /// </summary>
    public class AlipayOpenMiniInnerversionLastQueryModel : AlipayObject
    {
        /// <summary>
        /// 业务来源场景
        /// </summary>
        [JsonPropertyName("app_origin")]
        public string AppOrigin { get; set; }

        /// <summary>
        /// 端信息
        /// </summary>
        [JsonPropertyName("bundle_id")]
        public string BundleId { get; set; }

        /// <summary>
        /// 小程序ID，仅特殊场景使用，普通业务方无需关注该参数。
        /// </summary>
        [JsonPropertyName("mini_app_id")]
        public string MiniAppId { get; set; }

        /// <summary>
        /// 小程序主体
        /// </summary>
        [JsonPropertyName("pid")]
        public string Pid { get; set; }
    }
}
