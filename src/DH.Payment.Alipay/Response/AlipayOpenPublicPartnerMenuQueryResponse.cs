using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicPartnerMenuQueryResponse.
    /// </summary>
    public class AlipayOpenPublicPartnerMenuQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 服务窗菜单
        /// </summary>
        [JsonPropertyName("public_menu")]
        public string PublicMenu { get; set; }
    }
}
