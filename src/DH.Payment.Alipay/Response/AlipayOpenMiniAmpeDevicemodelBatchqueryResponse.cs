using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniAmpeDevicemodelBatchqueryResponse.
    /// </summary>
    public class AlipayOpenMiniAmpeDevicemodelBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 产品机型列表
        /// </summary>
        [JsonPropertyName("product_info")]
        public AmpeProductInfo ProductInfo { get; set; }
    }
}
