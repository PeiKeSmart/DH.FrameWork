using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// DeviceResultInfo Data Structure.
    /// </summary>
    public class DeviceResultInfo : AlipayObject
    {
        /// <summary>
        /// 设备id
        /// </summary>
        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        /// <summary>
        /// 业务数据类型
        /// </summary>
        [JsonPropertyName("device_label")]
        public string DeviceLabel { get; set; }
    }
}
