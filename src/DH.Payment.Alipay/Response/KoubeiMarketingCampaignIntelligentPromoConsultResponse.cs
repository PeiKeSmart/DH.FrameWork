using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignIntelligentPromoConsultResponse.
    /// </summary>
    public class KoubeiMarketingCampaignIntelligentPromoConsultResponse : AlipayResponse
    {
        /// <summary>
        /// 智能营销方案咨询的模型
        /// </summary>
        [JsonPropertyName("promo")]
        public IntelligentPromo Promo { get; set; }
    }
}
