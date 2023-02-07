using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCampaignActivityOfflineTriggerResponse.
    /// </summary>
    public class AlipayMarketingCampaignActivityOfflineTriggerResponse : AlipayResponse
    {
        /// <summary>
        /// 外部奖品ID
        /// </summary>
        [JsonPropertyName("out_prize_id")]
        public string OutPrizeId { get; set; }
    }
}
