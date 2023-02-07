using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// OverseasExtendParams Data Structure.
    /// </summary>
    public class OverseasExtendParams : AlipayObject
    {
        /// <summary>
        /// 商品明细列表
        /// </summary>
        [JsonPropertyName("goods_detail")]
        public string GoodsDetail { get; set; }
    }
}
