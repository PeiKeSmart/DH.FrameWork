using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossProdMyTestQueryResponse.
    /// </summary>
    public class AlipayBossProdMyTestQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 经度
        /// </summary>
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
    }
}
