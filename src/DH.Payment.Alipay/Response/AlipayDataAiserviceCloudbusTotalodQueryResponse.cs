using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusTotalodQueryResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusTotalodQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 客流总量结果
        /// </summary>
        [JsonPropertyName("result")]
        public CloudbusTotalOdItem Result { get; set; }
    }
}
