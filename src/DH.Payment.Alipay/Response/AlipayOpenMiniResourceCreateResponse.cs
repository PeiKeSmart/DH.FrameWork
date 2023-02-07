using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniResourceCreateResponse.
    /// </summary>
    public class AlipayOpenMiniResourceCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 流量位id
        /// </summary>
        [JsonPropertyName("resource_id")]
        public string ResourceId { get; set; }
    }
}
