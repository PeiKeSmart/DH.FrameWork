﻿using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOverseasOpenSchoolQueryResponse.
    /// </summary>
    public class AlipayOverseasOpenSchoolQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 留学汇款对isv返回结果
        /// </summary>
        [JsonPropertyName("result")]
        public TuitionISVResult Result { get; set; }

        /// <summary>
        /// 学校
        /// </summary>
        [JsonPropertyName("school_list")]
        public TuitionISVSchoolDTO SchoolList { get; set; }
    }
}
