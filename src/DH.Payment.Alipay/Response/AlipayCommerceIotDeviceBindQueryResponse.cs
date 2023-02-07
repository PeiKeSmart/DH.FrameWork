using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceIotDeviceBindQueryResponse.
    /// </summary>
    public class AlipayCommerceIotDeviceBindQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 设备绑定关系
        /// </summary>
        [JsonPropertyName("bind_info_list")]
        public List<IotDeviceBindInfo> BindInfoList { get; set; }
    }
}
