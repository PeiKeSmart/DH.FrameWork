using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayIserviceCognitiveKgopenQueryResponse.
    /// </summary>
    public class AlipayIserviceCognitiveKgopenQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}
