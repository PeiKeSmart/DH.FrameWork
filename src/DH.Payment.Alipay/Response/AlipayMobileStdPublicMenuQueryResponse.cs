using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMobileStdPublicMenuQueryResponse.
    /// </summary>
    public class AlipayMobileStdPublicMenuQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 所有菜单列表json串
        /// </summary>
        [JsonPropertyName("all_menu_list")]
        public string AllMenuList { get; set; }
    }
}
