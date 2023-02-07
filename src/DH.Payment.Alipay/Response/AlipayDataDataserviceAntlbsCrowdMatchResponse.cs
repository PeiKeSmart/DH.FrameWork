using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataDataserviceAntlbsCrowdMatchResponse.
    /// </summary>
    public class AlipayDataDataserviceAntlbsCrowdMatchResponse : AlipayResponse
    {
        /// <summary>
        /// 匹配结果，入参中每个客群码都会返回是否匹配，即使该客群不存在
        /// </summary>
        [JsonPropertyName("match_result")]
        public List<PromoxCrowdMatchModel> MatchResult { get; set; }
    }
}
