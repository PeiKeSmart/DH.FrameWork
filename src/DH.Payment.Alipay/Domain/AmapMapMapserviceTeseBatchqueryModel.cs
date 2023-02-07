using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AmapMapMapserviceTeseBatchqueryModel Data Structure.
    /// </summary>
    public class AmapMapMapserviceTeseBatchqueryModel : AlipayObject
    {
        /// <summary>
        /// 2
        /// </summary>
        [JsonPropertyName("sed")]
        public string Sed { get; set; }
    }
}
