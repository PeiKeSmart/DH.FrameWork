using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringPosPracticeModifyResponse.
    /// </summary>
    public class KoubeiCateringPosPracticeModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 做法id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
