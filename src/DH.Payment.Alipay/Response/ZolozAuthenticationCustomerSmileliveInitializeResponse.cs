using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// ZolozAuthenticationCustomerSmileliveInitializeResponse.
    /// </summary>
    public class ZolozAuthenticationCustomerSmileliveInitializeResponse : AlipayResponse
    {
        /// <summary>
        /// result
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}
