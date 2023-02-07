using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayPayAppSmartwearStatusQueryModel Data Structure.
    /// </summary>
    public class AlipayPayAppSmartwearStatusQueryModel : AlipayObject
    {
        /// <summary>
        /// 设备型号
        /// </summary>
        [JsonPropertyName("device_model")]
        public string DeviceModel { get; set; }
    }
}
