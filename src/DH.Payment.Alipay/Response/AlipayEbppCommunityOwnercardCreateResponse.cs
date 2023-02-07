using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppCommunityOwnercardCreateResponse.
    /// </summary>
    public class AlipayEbppCommunityOwnercardCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 支付宝证件id
        /// </summary>
        [JsonPropertyName("alipay_card_id")]
        public string AlipayCardId { get; set; }
    }
}
