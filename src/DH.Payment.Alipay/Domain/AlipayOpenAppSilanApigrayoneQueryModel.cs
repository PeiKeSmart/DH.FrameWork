using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenAppSilanApigrayoneQueryModel Data Structure.
    /// </summary>
    public class AlipayOpenAppSilanApigrayoneQueryModel : AlipayObject
    {
        /// <summary>
        /// param
        /// </summary>
        [JsonPropertyName("param_1")]
        public string Param1 { get; set; }
    }
}
