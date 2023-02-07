using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoCityserviceCityserviceEnergySendResponse.
    /// </summary>
    public class AlipayEcoCityserviceCityserviceEnergySendResponse : AlipayResponse
    {
        /// <summary>
        /// 发放的能量总额，单位：克
        /// </summary>
        [JsonPropertyName("total_energy")]
        public long TotalEnergy { get; set; }
    }
}
