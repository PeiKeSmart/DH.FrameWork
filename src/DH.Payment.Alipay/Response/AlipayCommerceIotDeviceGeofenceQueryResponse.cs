﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceIotDeviceGeofenceQueryResponse.
    /// </summary>
    public class AlipayCommerceIotDeviceGeofenceQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 返回线路上绑定的设备列表
        /// </summary>
        [JsonPropertyName("bind_device")]
        public List<string> BindDevice { get; set; }

        /// <summary>
        /// 地图围栏事件
        /// </summary>
        [JsonPropertyName("fence_events")]
        public List<FenceEvent> FenceEvents { get; set; }
    }
}
