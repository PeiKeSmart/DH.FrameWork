using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayBossFncSettleCaptureCreateModel Data Structure.
    /// </summary>
    public class AlipayBossFncSettleCaptureCreateModel : AlipayObject
    {
        /// <summary>
        /// 请款创建单列表
        /// </summary>
        [JsonPropertyName("capture_create_order_list")]
        public List<CaptureCreateOrder> CaptureCreateOrderList { get; set; }
    }
}
