﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicAdvertBatchqueryResponse.
    /// </summary>
    public class AlipayOpenPublicAdvertBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 广告位列表 。目前只有一个广告位。
        /// </summary>
        [JsonPropertyName("advert_list")]
        public List<Advert> AdvertList { get; set; }

        /// <summary>
        /// 广告组数量
        /// </summary>
        [JsonPropertyName("count")]
        public long Count { get; set; }
    }
}
