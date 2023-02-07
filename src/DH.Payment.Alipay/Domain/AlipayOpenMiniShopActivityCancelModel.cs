﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenMiniShopActivityCancelModel Data Structure.
    /// </summary>
    public class AlipayOpenMiniShopActivityCancelModel : AlipayObject
    {
        /// <summary>
        /// 业务处理流水号，调用活动创建接口时返回
        /// </summary>
        [JsonPropertyName("out_biz_id")]
        public string OutBizId { get; set; }
    }
}
