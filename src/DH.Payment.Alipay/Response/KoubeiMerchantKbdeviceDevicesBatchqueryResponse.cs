﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMerchantKbdeviceDevicesBatchqueryResponse.
    /// </summary>
    public class KoubeiMerchantKbdeviceDevicesBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 门店下设备列表
        /// </summary>
        [JsonPropertyName("device_info_list")]
        public List<DeviceInfo> DeviceInfoList { get; set; }
    }
}
