using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceTransportAdCreativeQueryResponse.
    /// </summary>
    public class AlipayCommerceTransportAdCreativeQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 广告创意详情；是否唯一(不需要)；用于通过创意ID查询创意详情；admgr广告创意查询返回
        /// </summary>
        [JsonPropertyName("ad_creative_result")]
        public AdCreativeResult AdCreativeResult { get; set; }
    }
}
