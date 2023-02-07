using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceIotAdvertiserDeviceConsultResponse.
    /// </summary>
    public class AlipayCommerceIotAdvertiserDeviceConsultResponse : AlipayResponse
    {
        /// <summary>
        /// 设备关联数据
        /// </summary>
        [JsonPropertyName("result")]
        public List<DeviceRelationData> Result { get; set; }
    }
}
