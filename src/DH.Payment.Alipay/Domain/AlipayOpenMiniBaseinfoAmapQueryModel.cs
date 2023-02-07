using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenMiniBaseinfoAmapQueryModel Data Structure.
    /// </summary>
    public class AlipayOpenMiniBaseinfoAmapQueryModel : AlipayObject
    {
        /// <summary>
        /// 小程序应用id
        /// </summary>
        [JsonPropertyName("appid")]
        public string Appid { get; set; }
    }
}
