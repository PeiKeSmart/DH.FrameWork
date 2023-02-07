using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// ZhimaCreditPeZmgoTbsignVerifyResponse.
    /// </summary>
    public class ZhimaCreditPeZmgoTbsignVerifyResponse : AlipayResponse
    {
        /// <summary>
        /// 签约核身返回的JSON信息
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}
