﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceIotSupplierAssetCreateModel Data Structure.
    /// </summary>
    public class AlipayCommerceIotSupplierAssetCreateModel : AlipayObject
    {
        /// <summary>
        /// 供应商设备信息
        /// </summary>
        [JsonPropertyName("record")]
        public SupplierAssetResponse Record { get; set; }
    }
}
