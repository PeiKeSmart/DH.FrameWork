using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusSchedualtasktimeQueryResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusSchedualtasktimeQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        [JsonPropertyName("result")]
        public ScheduleTimeResult Result { get; set; }
    }
}
