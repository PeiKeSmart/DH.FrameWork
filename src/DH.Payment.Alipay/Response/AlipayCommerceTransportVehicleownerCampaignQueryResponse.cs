using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceTransportVehicleownerCampaignQueryResponse.
    /// </summary>
    public class AlipayCommerceTransportVehicleownerCampaignQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 活动信息
        /// </summary>
        [JsonPropertyName("market_info")]
        public MarketInfo MarketInfo { get; set; }
    }
}
