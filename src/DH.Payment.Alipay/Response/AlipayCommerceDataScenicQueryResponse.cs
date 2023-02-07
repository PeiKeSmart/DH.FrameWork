using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceDataScenicQueryResponse.
    /// </summary>
    public class AlipayCommerceDataScenicQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 景区分页信息
        /// </summary>
        [JsonPropertyName("pagination_scenic_info")]
        public PaginationScenicInfo PaginationScenicInfo { get; set; }
    }
}
