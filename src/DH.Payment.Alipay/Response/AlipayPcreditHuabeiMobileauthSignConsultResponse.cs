﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayPcreditHuabeiMobileauthSignConsultResponse.
    /// </summary>
    public class AlipayPcreditHuabeiMobileauthSignConsultResponse : AlipayResponse
    {
        /// <summary>
        /// 模板查询返回JSON信息，参考 PcreditAuthSignApplyResult
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}
