using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataDataserviceVerificationResultSendResponse.
    /// </summary>
    public class AlipayDataDataserviceVerificationResultSendResponse : AlipayResponse
    {
        /// <summary>
        /// 反馈是否成功
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
