using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayIserviceCcmRoleCreateResponse.
    /// </summary>
    public class AlipayIserviceCcmRoleCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 角色id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
