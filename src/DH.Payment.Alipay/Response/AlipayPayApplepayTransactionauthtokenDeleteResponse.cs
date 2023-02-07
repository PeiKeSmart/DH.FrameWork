using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayPayApplepayTransactionauthtokenDeleteResponse.
    /// </summary>
    public class AlipayPayApplepayTransactionauthtokenDeleteResponse : AlipayResponse
    {
        /// <summary>
        /// 响应报文头部
        /// </summary>
        [JsonPropertyName("response_header")]
        public BaseOpenApiResponseHeaderDTO ResponseHeader { get; set; }
    }
}
