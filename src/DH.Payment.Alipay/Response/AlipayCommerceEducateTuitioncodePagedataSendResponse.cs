﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateTuitioncodePagedataSendResponse.
    /// </summary>
    public class AlipayCommerceEducateTuitioncodePagedataSendResponse : AlipayResponse
    {
        /// <summary>
        /// 页面数据录入id
        /// </summary>
        [JsonPropertyName("pagedata_id")]
        public string PagedataId { get; set; }
    }
}
