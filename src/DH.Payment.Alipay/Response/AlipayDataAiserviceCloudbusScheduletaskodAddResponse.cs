using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusScheduletaskodAddResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusScheduletaskodAddResponse : AlipayResponse
    {
        /// <summary>
        /// 任务结果
        /// </summary>
        [JsonPropertyName("result")]
        public CloudbusCommonResult Result { get; set; }
    }
}
