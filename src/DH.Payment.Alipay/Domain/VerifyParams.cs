using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// VerifyParams Data Structure.
    /// </summary>
    public class VerifyParams : AlipayObject
    {
        /// <summary>
        /// 用户证件号后4位
        /// </summary>
        [JsonPropertyName("cert_no")]
        public string CertNo { get; set; }
    }
}
