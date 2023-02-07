using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdieSitesBatchqueryResponse.
    /// </summary>
    public class AlipayMarketingToolFengdieSitesBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 获取云凤蝶站点列表返回值模型
        /// </summary>
        [JsonPropertyName("data")]
        public FengdieSitesListRespModel Data { get; set; }
    }
}
