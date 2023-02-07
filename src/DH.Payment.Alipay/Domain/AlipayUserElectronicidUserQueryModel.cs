using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayUserElectronicidUserQueryModel Data Structure.
    /// </summary>
    public class AlipayUserElectronicidUserQueryModel : AlipayObject
    {
        /// <summary>
        /// 用户码码串
        /// </summary>
        [JsonPropertyName("barcode")]
        public string Barcode { get; set; }
    }
}
