using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiAdvertDataConfQueryResponse.
    /// </summary>
    public class KoubeiAdvertDataConfQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 渠道类型配置
        /// </summary>
        [JsonPropertyName("channel_types")]
        public List<KbadvertChannelTypeResponse> ChannelTypes { get; set; }

        /// <summary>
        /// 分佣配置金额
        /// </summary>
        [JsonPropertyName("commission_limits")]
        public List<KbadvertCommissionLimit> CommissionLimits { get; set; }
    }
}
