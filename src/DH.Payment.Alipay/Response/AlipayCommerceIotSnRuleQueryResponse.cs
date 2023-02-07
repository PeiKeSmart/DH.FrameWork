using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceIotSnRuleQueryResponse.
    /// </summary>
    public class AlipayCommerceIotSnRuleQueryResponse : AlipayResponse
    {
        /// <summary>
        /// profile规则列表
        /// </summary>
        [JsonPropertyName("rule_list")]
        public List<ProfileRuleInfo> RuleList { get; set; }
    }
}
