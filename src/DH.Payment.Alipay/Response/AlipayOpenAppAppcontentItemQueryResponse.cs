using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenAppAppcontentItemQueryResponse.
    /// </summary>
    public class AlipayOpenAppAppcontentItemQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        [JsonPropertyName("items")]
        public List<AppContentItem> Items { get; set; }

        /// <summary>
        /// 总商品数
        /// </summary>
        [JsonPropertyName("total")]
        public long Total { get; set; }
    }
}
