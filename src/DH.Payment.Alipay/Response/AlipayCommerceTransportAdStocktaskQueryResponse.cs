using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceTransportAdStocktaskQueryResponse.
    /// </summary>
    public class AlipayCommerceTransportAdStocktaskQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 库存任务执行情况
        /// </summary>
        [JsonPropertyName("stock_task")]
        public StockTask StockTask { get; set; }
    }
}
