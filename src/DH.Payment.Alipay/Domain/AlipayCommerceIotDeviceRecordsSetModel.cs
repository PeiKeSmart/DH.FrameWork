﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceIotDeviceRecordsSetModel Data Structure.
    /// </summary>
    public class AlipayCommerceIotDeviceRecordsSetModel : AlipayObject
    {
        /// <summary>
        /// 设备档案文件
        /// </summary>
        [JsonPropertyName("device_record_files")]
        public List<DeviceRecordFile> DeviceRecordFiles { get; set; }

        /// <summary>
        /// 设备档案拓展信息
        /// </summary>
        [JsonPropertyName("ext_params")]
        public DeviceExtParams ExtParams { get; set; }

        /// <summary>
        /// 设备档案管理场景 IOT_DEVICE_RECORDS_G1(极简绑定) IOT_DEVICE_RECORDS_G3_INDIRECT(间连三绑定)  IOT_DEVICE_RECORDS_G3_DIRECT(直连三绑定) IOT_DEVICE_RECORDS_DELETE(解绑) IOT_DEVICE_RECORDS_QUERY（绑定查询）
        /// </summary>
        [JsonPropertyName("scene_code")]
        public string SceneCode { get; set; }

        /// <summary>
        /// CREATE UPDATE DEFAULT
        /// </summary>
        [JsonPropertyName("scene_params")]
        public string SceneParams { get; set; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        [JsonPropertyName("sn")]
        public string Sn { get; set; }

        /// <summary>
        /// 设备供应商ID
        /// </summary>
        [JsonPropertyName("supplier_id")]
        public string SupplierId { get; set; }
    }
}
