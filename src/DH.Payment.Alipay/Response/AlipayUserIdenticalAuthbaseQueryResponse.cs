using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayUserIdenticalAuthbaseQueryResponse.
    /// </summary>
    public class AlipayUserIdenticalAuthbaseQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 是否是同人账号
        /// </summary>
        [JsonPropertyName("identical")]
        public bool Identical { get; set; }
    }
}
