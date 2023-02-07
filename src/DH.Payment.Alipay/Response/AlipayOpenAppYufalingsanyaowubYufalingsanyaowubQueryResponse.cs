using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenAppYufalingsanyaowubYufalingsanyaowubQueryResponse.
    /// </summary>
    public class AlipayOpenAppYufalingsanyaowubYufalingsanyaowubQueryResponse : AlipayResponse
    {
        /// <summary>
        /// yufaa
        /// </summary>
        [JsonPropertyName("yufaa")]
        public string Yufaa { get; set; }
    }
}
