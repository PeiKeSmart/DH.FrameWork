﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayPcreditHuabeiMobileauthSignConfirmResponse.
    /// </summary>
    public class AlipayPcreditHuabeiMobileauthSignConfirmResponse : AlipayResponse
    {
        /// <summary>
        /// 模板查询返回JSON信息，参考 PcreditAuthSignConfirmResult
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}
