using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoActivityRecycleSendResponse.
    /// </summary>
    public class AlipayEcoActivityRecycleSendResponse : AlipayResponse
    {
        /// <summary>
        /// 发放的能量总额
        /// </summary>
        [JsonPropertyName("full_energy")]
        public long FullEnergy { get; set; }
    }
}
