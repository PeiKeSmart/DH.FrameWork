using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataDataserviceAdCreativeQueryResponse.
    /// </summary>
    public class AlipayDataDataserviceAdCreativeQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 创意详情
        /// </summary>
        [JsonPropertyName("creative_detail")]
        public CreativeDetail CreativeDetail { get; set; }
    }
}
