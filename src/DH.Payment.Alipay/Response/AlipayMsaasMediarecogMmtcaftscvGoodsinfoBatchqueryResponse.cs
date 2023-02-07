using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMsaasMediarecogMmtcaftscvGoodsinfoBatchqueryResponse.
    /// </summary>
    public class AlipayMsaasMediarecogMmtcaftscvGoodsinfoBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 商品信息列表
        /// </summary>
        [JsonPropertyName("goods_infos")]
        public List<AlgorithmGoodsInfo> GoodsInfos { get; set; }
    }
}
