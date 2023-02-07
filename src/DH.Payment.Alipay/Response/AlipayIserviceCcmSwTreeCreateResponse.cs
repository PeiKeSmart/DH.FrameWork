using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayIserviceCcmSwTreeCreateResponse.
    /// </summary>
    public class AlipayIserviceCcmSwTreeCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 类目ID
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
