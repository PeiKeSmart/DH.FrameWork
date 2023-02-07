using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceIotGroupBatchqueryResponse.
    /// </summary>
    public class AlipayCommerceIotGroupBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 分组列表
        /// </summary>
        [JsonPropertyName("groups")]
        public List<DeviceGroup> Groups { get; set; }

        /// <summary>
        /// 设备分组总数
        /// </summary>
        [JsonPropertyName("total")]
        public List<long> Total { get; set; }
    }
}
