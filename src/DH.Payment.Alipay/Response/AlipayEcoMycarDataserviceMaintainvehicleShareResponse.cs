using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoMycarDataserviceMaintainvehicleShareResponse.
    /// </summary>
    public class AlipayEcoMycarDataserviceMaintainvehicleShareResponse : AlipayResponse
    {
        /// <summary>
        /// 车辆详细信息
        /// </summary>
        [JsonPropertyName("info")]
        public MaintainVehicleInfo Info { get; set; }
    }
}
