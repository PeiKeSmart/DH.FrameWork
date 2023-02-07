using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdieTemplateBatchqueryResponse.
    /// </summary>
    public class AlipayMarketingToolFengdieTemplateBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 模板详情列表
        /// </summary>
        [JsonPropertyName("data")]
        public FengdieTemplateListRespModel Data { get; set; }
    }
}
