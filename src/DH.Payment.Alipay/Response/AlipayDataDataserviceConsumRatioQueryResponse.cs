using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataDataserviceConsumRatioQueryResponse.
    /// </summary>
    public class AlipayDataDataserviceConsumRatioQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 占比数据接口返回数据结构
        /// </summary>
        [JsonPropertyName("ratio_detail")]
        public RatioDetail RatioDetail { get; set; }
    }
}
