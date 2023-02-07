using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniDataPageQueryResponse.
    /// </summary>
    public class AlipayOpenMiniDataPageQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 页面访问数据结果
        /// </summary>
        [JsonPropertyName("page_visit_data_list_response")]
        public List<PageVisitDataResponse> PageVisitDataListResponse { get; set; }
    }
}
