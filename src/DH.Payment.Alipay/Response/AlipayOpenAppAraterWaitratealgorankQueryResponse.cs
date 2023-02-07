using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenAppAraterWaitratealgorankQueryResponse.
    /// </summary>
    public class AlipayOpenAppAraterWaitratealgorankQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 待评价列表打分结果
        /// </summary>
        [JsonPropertyName("wait_rate_rank_items")]
        public List<WaitRateAlgoItem> WaitRateRankItems { get; set; }
    }
}
