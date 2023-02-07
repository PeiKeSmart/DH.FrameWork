﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AntMerchantExpandIndirectActivityQueryModel Data Structure.
    /// </summary>
    public class AntMerchantExpandIndirectActivityQueryModel : AlipayObject
    {
        /// <summary>
        /// 商户在支付宝入驻成功后，生成的支付宝内全局唯一的商户编号
        /// </summary>
        [JsonPropertyName("sub_merchant_id")]
        public string SubMerchantId { get; set; }
    }
}
