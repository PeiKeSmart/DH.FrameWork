﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenMiniMorphoAppOfflineModel Data Structure.
    /// </summary>
    public class AlipayOpenMiniMorphoAppOfflineModel : AlipayObject
    {
        /// <summary>
        /// 闪蝶应用ID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// 闪蝶身份验证信息
        /// </summary>
        [JsonPropertyName("identity")]
        public MorphoIdentity Identity { get; set; }
    }
}
