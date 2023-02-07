using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMobilePublicFollowAddResponse.
    /// </summary>
    public class AlipayMobilePublicFollowAddResponse : AlipayResponse
    {
        /// <summary>
        /// 成功与否的标志
        /// </summary>
        [JsonPropertyName("code")]
        public new string Code { get; set; }
    }
}
