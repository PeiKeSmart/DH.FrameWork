using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataServiceOpenAnalyzeResponse.
    /// </summary>
    public class AlipayDataServiceOpenAnalyzeResponse : AlipayResponse
    {
        /// <summary>
        /// 调用返回
        /// </summary>
        [JsonPropertyName("result")]
        public AlipayDataServiceResult Result { get; set; }
    }
}
