using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// ZhimaCreditPeZmgoTbsignVerifyModel Data Structure.
    /// </summary>
    public class ZhimaCreditPeZmgoTbsignVerifyModel : AlipayObject
    {
        /// <summary>
        /// 请求参数，其中paramContent是base64编码串
        /// </summary>
        [JsonPropertyName("request")]
        public string Request { get; set; }
    }
}
