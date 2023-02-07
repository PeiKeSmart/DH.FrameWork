using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusTransitorridorQueryResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusTransitorridorQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        [JsonPropertyName("result")]
        public CloudbusTransitResultItem Result { get; set; }
    }
}
