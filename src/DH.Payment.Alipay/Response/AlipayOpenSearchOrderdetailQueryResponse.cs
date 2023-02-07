using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenSearchOrderdetailQueryResponse.
    /// </summary>
    public class AlipayOpenSearchOrderdetailQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 申请单详情
        /// </summary>
        [JsonPropertyName("data")]
        public SearchOrderDetailData Data { get; set; }
    }
}
