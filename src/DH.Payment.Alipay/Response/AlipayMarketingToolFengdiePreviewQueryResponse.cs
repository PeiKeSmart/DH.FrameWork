using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdiePreviewQueryResponse.
    /// </summary>
    public class AlipayMarketingToolFengdiePreviewQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 站点页面预览数据返回值
        /// </summary>
        [JsonPropertyName("data")]
        public FengdiePreviewQueryRespModel Data { get; set; }
    }
}
