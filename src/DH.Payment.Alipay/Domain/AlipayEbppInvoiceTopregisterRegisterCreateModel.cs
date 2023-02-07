﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEbppInvoiceTopregisterRegisterCreateModel Data Structure.
    /// </summary>
    public class AlipayEbppInvoiceTopregisterRegisterCreateModel : AlipayObject
    {
        /// <summary>
        /// 创建工单请求
        /// </summary>
        [JsonPropertyName("create_request")]
        public InvoiceRegisterCreateDTO CreateRequest { get; set; }
    }
}
