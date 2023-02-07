﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringKdsInfoQueryResponse.
    /// </summary>
    public class KoubeiCateringKdsInfoQueryResponse : AlipayResponse
    {
        /// <summary>
        /// kds 配置信息列表
        /// </summary>
        [JsonPropertyName("kds_info_model_list")]
        public List<KdsInfoModel> KdsInfoModelList { get; set; }
    }
}
