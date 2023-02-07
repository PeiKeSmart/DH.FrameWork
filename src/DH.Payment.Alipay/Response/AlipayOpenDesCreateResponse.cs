using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenDesCreateResponse.
    /// </summary>
    public class AlipayOpenDesCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 1
        /// </summary>
        [JsonPropertyName("ces")]
        public GavintestNewLeveaOne Ces { get; set; }
    }
}
