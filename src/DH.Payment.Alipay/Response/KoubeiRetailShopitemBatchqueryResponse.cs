using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiRetailShopitemBatchqueryResponse.
    /// </summary>
    public class KoubeiRetailShopitemBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 店铺商品集合
        /// </summary>
        [JsonPropertyName("shopitemlist")]
        public List<ExtShopItem> Shopitemlist { get; set; }
    }
}
