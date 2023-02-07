using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusMetrotimeQueryResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusMetrotimeQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        [JsonPropertyName("result")]
        public MetroOdDetailResult Result { get; set; }
    }
}
