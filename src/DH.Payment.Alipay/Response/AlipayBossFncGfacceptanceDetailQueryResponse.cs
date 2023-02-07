using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossFncGfacceptanceDetailQueryResponse.
    /// </summary>
    public class AlipayBossFncGfacceptanceDetailQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 业财受理详情查询结果
        /// </summary>
        [JsonPropertyName("result")]
        public GFAOpenAPIDetailQueryResult Result { get; set; }
    }
}
