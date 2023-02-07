using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceIotDeviceTraceQueryResponse.
    /// </summary>
    public class AlipayCommerceIotDeviceTraceQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 返回了这段时间内设备的轨迹
        /// </summary>
        [JsonPropertyName("trace")]
        public string Trace { get; set; }
    }
}
