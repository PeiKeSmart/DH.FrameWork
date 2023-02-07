using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppProdmodeDropdataQueryResponse.
    /// </summary>
    public class AlipayEbppProdmodeDropdataQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 返回业务类型、子业务类型、产品模式及销账模式下拉列表
        /// </summary>
        [JsonPropertyName("data_list")]
        public List<BizListDataInfo> DataList { get; set; }
    }
}
