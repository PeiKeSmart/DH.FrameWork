using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayZmScoreZrankResult Data Structure.
    /// </summary>
    public class AlipayZmScoreZrankResult : AlipayObject
    {
        /// <summary>
        /// 芝麻分分段 Z0-Z7
        /// </summary>
        [JsonPropertyName("zrank")]
        public string Zrank { get; set; }
    }
}
