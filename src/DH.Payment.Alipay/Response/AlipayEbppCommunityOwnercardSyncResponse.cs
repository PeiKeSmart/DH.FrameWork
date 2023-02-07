using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppCommunityOwnercardSyncResponse.
    /// </summary>
    public class AlipayEbppCommunityOwnercardSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 支付宝证件id
        /// </summary>
        [JsonPropertyName("alipay_card_id")]
        public string AlipayCardId { get; set; }
    }
}
