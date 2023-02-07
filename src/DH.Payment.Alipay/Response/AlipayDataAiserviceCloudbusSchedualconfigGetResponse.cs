using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusSchedualconfigGetResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusSchedualconfigGetResponse : AlipayResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        [JsonPropertyName("result")]
        public ScheduleConfigGetResult Result { get; set; }
    }
}
