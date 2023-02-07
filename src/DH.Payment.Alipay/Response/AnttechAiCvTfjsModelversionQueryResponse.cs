using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AnttechAiCvTfjsModelversionQueryResponse.
    /// </summary>
    public class AnttechAiCvTfjsModelversionQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 对应模型最新版本号
        /// </summary>
        [JsonPropertyName("model_version")]
        public string ModelVersion { get; set; }
    }
}
