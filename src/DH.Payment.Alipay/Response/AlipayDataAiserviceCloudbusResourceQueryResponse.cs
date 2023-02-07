using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusResourceQueryResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusResourceQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        [JsonPropertyName("result")]
        public ScheduleResResult Result { get; set; }
    }
}
