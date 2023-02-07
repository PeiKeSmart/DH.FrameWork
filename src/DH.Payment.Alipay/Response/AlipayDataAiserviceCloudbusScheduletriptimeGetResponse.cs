using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusScheduletriptimeGetResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusScheduletriptimeGetResponse : AlipayResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        [JsonPropertyName("result")]
        public ScheduleTripResult Result { get; set; }
    }
}
