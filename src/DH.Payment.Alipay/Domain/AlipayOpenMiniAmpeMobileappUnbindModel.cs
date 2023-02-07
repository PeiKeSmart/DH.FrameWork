﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenMiniAmpeMobileappUnbindModel Data Structure.
    /// </summary>
    public class AlipayOpenMiniAmpeMobileappUnbindModel : AlipayObject
    {
        /// <summary>
        /// 移动应用id
        /// </summary>
        [JsonPropertyName("mobile_app_id")]
        public string MobileAppId { get; set; }

        /// <summary>
        /// 产品ID列表
        /// </summary>
        [JsonPropertyName("product_id_list")]
        public List<long> ProductIdList { get; set; }

        /// <summary>
        /// 场景码，申请后平台分配
        /// </summary>
        [JsonPropertyName("scene_code")]
        public string SceneCode { get; set; }
    }
}
