using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdieActivityQueryResponse.
    /// </summary>
    public class AlipayMarketingToolFengdieActivityQueryResponse : AlipayResponse
    {
        /// <summary>
        /// H5应用详情
        /// </summary>
        [JsonPropertyName("activity")]
        public FengdieActivity Activity { get; set; }
    }
}
