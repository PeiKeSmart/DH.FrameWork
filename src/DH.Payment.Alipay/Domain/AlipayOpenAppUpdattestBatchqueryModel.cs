using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenAppUpdattestBatchqueryModel Data Structure.
    /// </summary>
    public class AlipayOpenAppUpdattestBatchqueryModel : AlipayObject
    {
        /// <summary>
        /// 64
        /// </summary>
        [JsonPropertyName("campus_card")]
        public string CampusCard { get; set; }

        /// <summary>
        /// 21
        /// </summary>
        [JsonPropertyName("s")]
        public string S { get; set; }
    }
}
