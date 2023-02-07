using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayPayCodecQrcodecacheAddResponse.
    /// </summary>
    public class AlipayPayCodecQrcodecacheAddResponse : AlipayResponse
    {
        /// <summary>
        /// true或者false
        /// </summary>
        [JsonPropertyName("success")]
        public string Success { get; set; }
    }
}
