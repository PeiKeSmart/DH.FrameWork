using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppInspectSessionQueryResponse.
    /// </summary>
    public class AlipayEbppInspectSessionQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 登录ID
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
