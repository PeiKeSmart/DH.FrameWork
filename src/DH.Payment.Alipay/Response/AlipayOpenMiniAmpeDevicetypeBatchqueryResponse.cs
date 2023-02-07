using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniAmpeDevicetypeBatchqueryResponse.
    /// </summary>
    public class AlipayOpenMiniAmpeDevicetypeBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 设备类型列表
        /// </summary>
        [JsonPropertyName("device_type_list")]
        public List<AmpeDeviceTypeInfo> DeviceTypeList { get; set; }
    }
}
