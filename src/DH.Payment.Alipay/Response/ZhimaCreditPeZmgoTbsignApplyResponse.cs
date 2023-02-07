using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// ZhimaCreditPeZmgoTbsignApplyResponse.
    /// </summary>
    public class ZhimaCreditPeZmgoTbsignApplyResponse : AlipayResponse
    {
        /// <summary>
        /// 签约申请返回的JSON信息
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}
