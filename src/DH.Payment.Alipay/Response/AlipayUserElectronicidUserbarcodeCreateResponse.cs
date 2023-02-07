using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayUserElectronicidUserbarcodeCreateResponse.
    /// </summary>
    public class AlipayUserElectronicidUserbarcodeCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 用户电子身份证码串
        /// </summary>
        [JsonPropertyName("barcode")]
        public string Barcode { get; set; }
    }
}
