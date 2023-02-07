using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// MybankCreditSupplychainArAdmitQueryResponse.
    /// </summary>
    public class MybankCreditSupplychainArAdmitQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 准入/不准入：true/false
        /// </summary>
        [JsonPropertyName("admit")]
        public bool Admit { get; set; }

        /// <summary>
        /// 不准入说明
        /// </summary>
        [JsonPropertyName("context")]
        public string Context { get; set; }
    }
}
