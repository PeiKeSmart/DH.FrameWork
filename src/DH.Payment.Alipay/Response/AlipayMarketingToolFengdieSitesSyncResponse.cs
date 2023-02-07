using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdieSitesSyncResponse.
    /// </summary>
    public class AlipayMarketingToolFengdieSitesSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 返回站点升级是否成功
        /// </summary>
        [JsonPropertyName("data")]
        public FengdieSuccessRespModel Data { get; set; }
    }
}
