using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceIotDapplyDevicetradestatlistQueryResponse.
    /// </summary>
    public class AlipayCommerceIotDapplyDevicetradestatlistQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 设备交易统计信息
        /// </summary>
        [JsonPropertyName("devicetradestatlist")]
        public DeviceTradeInfoList Devicetradestatlist { get; set; }

        /// <summary>
        /// 表示返回的列表总数
        /// </summary>
        [JsonPropertyName("total_count")]
        public long TotalCount { get; set; }
    }
}
