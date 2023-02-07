using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignItemQueryResponse.
    /// </summary>
    public class KoubeiMarketingCampaignItemQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 商品详情，包含门店信息
        /// </summary>
        [JsonPropertyName("item")]
        public PromoItemInfo Item { get; set; }
    }
}
