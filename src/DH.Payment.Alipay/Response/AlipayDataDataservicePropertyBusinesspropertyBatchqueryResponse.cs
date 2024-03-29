﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataDataservicePropertyBusinesspropertyBatchqueryResponse.
    /// </summary>
    public class AlipayDataDataservicePropertyBusinesspropertyBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 业务画像标签元信息列表
        /// </summary>
        [JsonPropertyName("business_propertys")]
        public List<BusinessPropertyDTO> BusinessPropertys { get; set; }
    }
}
