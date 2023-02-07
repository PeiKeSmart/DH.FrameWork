﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayFundTransInvoiceFromisvnotifyModifyResponse.
    /// </summary>
    public class AlipayFundTransInvoiceFromisvnotifyModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 票据详情页url
        /// </summary>
        [JsonPropertyName("invoice_url")]
        public string InvoiceUrl { get; set; }
    }
}
