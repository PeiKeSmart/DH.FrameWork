﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateTrainCourseCreateResponse.
    /// </summary>
    public class AlipayCommerceEducateTrainCourseCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 支付宝内部课程id
        /// </summary>
        [JsonPropertyName("course_id")]
        public string CourseId { get; set; }
    }
}
