using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniMorphoApplistBatchqueryResponse.
    /// </summary>
    public class AlipayOpenMiniMorphoApplistBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 应用列表
        /// </summary>
        [JsonPropertyName("data_list")]
        public List<MorphoAppItem> DataList { get; set; }

        /// <summary>
        /// 批量查询分页信息
        /// </summary>
        [JsonPropertyName("paginator")]
        public MorphoPaginator Paginator { get; set; }
    }
}
