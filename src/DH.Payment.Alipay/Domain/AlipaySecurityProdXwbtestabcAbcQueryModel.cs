using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipaySecurityProdXwbtestabcAbcQueryModel Data Structure.
    /// </summary>
    public class AlipaySecurityProdXwbtestabcAbcQueryModel : AlipayObject
    {
        /// <summary>
        /// 1
        /// </summary>
        [JsonPropertyName("xwb")]
        public string Xwb { get; set; }
    }
}
