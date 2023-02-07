﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiCateringKdsInfoModifyModel Data Structure.
    /// </summary>
    public class KoubeiCateringKdsInfoModifyModel : AlipayObject
    {
        /// <summary>
        /// kds配置详情
        /// </summary>
        [JsonPropertyName("kds_info_model_list")]
        public List<KdsInfoModel> KdsInfoModelList { get; set; }
    }
}
