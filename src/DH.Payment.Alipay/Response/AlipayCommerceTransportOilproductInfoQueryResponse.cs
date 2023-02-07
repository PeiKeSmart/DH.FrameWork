using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceTransportOilproductInfoQueryResponse.
    /// </summary>
    public class AlipayCommerceTransportOilproductInfoQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 油站列表
        /// </summary>
        [JsonPropertyName("oil_station_models")]
        public OilStationDetails OilStationModels { get; set; }
    }
}
