﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateStucodeUserQueryResponse.
    /// </summary>
    public class AlipayCommerceEducateStucodeUserQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 学籍档案
        /// </summary>
        [JsonPropertyName("archive")]
        public StuStatusArchive Archive { get; set; }

        /// <summary>
        /// 是否通过学生认证
        /// </summary>
        [JsonPropertyName("stu_certify")]
        public bool StuCertify { get; set; }
    }
}
