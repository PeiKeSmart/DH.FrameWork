using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// OverseasGoods Data Structure.
    /// </summary>
    public class OverseasGoods : AlipayObject
    {
        /// <summary>
        /// 商品价格，单位为元
        /// </summary>
        [JsonPropertyName("goods_amount")]
        public string GoodsAmount { get; set; }

        /// <summary>
        /// 商品的编号
        /// </summary>
        [JsonPropertyName("goods_id")]
        public string GoodsId { get; set; }
    }
}
