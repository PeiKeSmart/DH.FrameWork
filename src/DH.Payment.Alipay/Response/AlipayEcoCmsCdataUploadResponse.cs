using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoCmsCdataUploadResponse.
    /// </summary>
    public class AlipayEcoCmsCdataUploadResponse : AlipayResponse
    {
        /// <summary>
        /// 投放消息ID
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
