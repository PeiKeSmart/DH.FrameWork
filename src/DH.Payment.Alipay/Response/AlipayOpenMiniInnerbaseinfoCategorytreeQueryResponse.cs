using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniInnerbaseinfoCategorytreeQueryResponse.
    /// </summary>
    public class AlipayOpenMiniInnerbaseinfoCategorytreeQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 小程序一级类目列表
        /// </summary>
        [JsonPropertyName("category_list")]
        public List<MiniAppFirstCategoryInfo> CategoryList { get; set; }
    }
}
