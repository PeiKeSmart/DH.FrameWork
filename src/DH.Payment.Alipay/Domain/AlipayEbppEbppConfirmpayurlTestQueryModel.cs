using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEbppEbppConfirmpayurlTestQueryModel Data Structure.
    /// </summary>
    public class AlipayEbppEbppConfirmpayurlTestQueryModel : AlipayObject
    {
        /// <summary>
        /// ss
        /// </summary>
        [JsonPropertyName("tess")]
        public string Tess { get; set; }
    }
}
