﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMerchantDepartmentLabelQueryResponse.
    /// </summary>
    public class KoubeiMerchantDepartmentLabelQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 人员组织可用的部门标签列表
        /// </summary>
        [JsonPropertyName("department_labels")]
        public List<DepartmentLabel> DepartmentLabels { get; set; }
    }
}
