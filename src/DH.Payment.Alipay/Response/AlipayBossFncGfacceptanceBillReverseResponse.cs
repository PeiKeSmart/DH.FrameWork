using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossFncGfacceptanceBillReverseResponse.
    /// </summary>
    public class AlipayBossFncGfacceptanceBillReverseResponse : AlipayResponse
    {
        /// <summary>
        /// 业财受理结果
        /// </summary>
        [JsonPropertyName("result")]
        public GFAOpenAPIAcceptanceResult Result { get; set; }
    }
}
