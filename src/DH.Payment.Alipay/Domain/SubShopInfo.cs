using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// SubShopInfo Data Structure.
    /// </summary>
    public class SubShopInfo : AlipayObject
    {
        /// <summary>
        /// 门店信息集合
        /// </summary>
        [JsonPropertyName("shop_list")]
        public List<Shop> ShopList { get; set; }
    }
}
