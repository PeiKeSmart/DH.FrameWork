﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenMiniInnerclientinfoQueryModel Data Structure.
    /// </summary>
    public class AlipayOpenMiniInnerclientinfoQueryModel : AlipayObject
    {
        /// <summary>
        /// 端创建时提供的bundle_id
        /// </summary>
        [JsonPropertyName("bundle_id")]
        public string BundleId { get; set; }
    }
}
