﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppDetectCreateResponse.
    /// </summary>
    public class AlipayEbppDetectCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 外部业务标识，同入参的out_biz_no
        /// </summary>
        [JsonPropertyName("out_biz_no")]
        public string OutBizNo { get; set; }
    }
}
