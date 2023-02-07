using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolSigninSubmitResponse.
    /// </summary>
    public class AlipayMarketingToolSigninSubmitResponse : AlipayResponse
    {
        /// <summary>
        /// 发奖单据。
        /// </summary>
        [JsonPropertyName("prize_orders")]
        public List<PrizeOrderVO> PrizeOrders { get; set; }
    }
}
