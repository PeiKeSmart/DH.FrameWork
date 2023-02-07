using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayPcreditHuabeiMobileauthTemplatetextQueryModel Data Structure.
    /// </summary>
    public class AlipayPcreditHuabeiMobileauthTemplatetextQueryModel : AlipayObject
    {
        /// <summary>
        /// 请求参数
        /// </summary>
        [JsonPropertyName("request")]
        public string Request { get; set; }
    }
}
