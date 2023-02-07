using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusSchedualconfigSetResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusSchedualconfigSetResponse : AlipayResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        [JsonPropertyName("result")]
        public ScheduleConfigResult Result { get; set; }
    }
}
