﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenMiniBizdataTemplatemessageUploadModel Data Structure.
    /// </summary>
    public class AlipayOpenMiniBizdataTemplatemessageUploadModel : AlipayObject
    {
        /// <summary>
        /// 服务信息列表，列表大小不超过50
        /// </summary>
        [JsonPropertyName("biz_data_list")]
        public List<UserAppBizDataInfo> BizDataList { get; set; }
    }
}
