using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusScheduletasklistQueryResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusScheduletasklistQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        [JsonPropertyName("result")]
        public ScheduleListResult Result { get; set; }
    }
}
