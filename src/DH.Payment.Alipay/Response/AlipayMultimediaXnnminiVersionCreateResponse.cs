using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMultimediaXnnminiVersionCreateResponse.
    /// </summary>
    public class AlipayMultimediaXnnminiVersionCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 版本信息标识id
        /// </summary>
        [JsonPropertyName("version_id")]
        public string VersionId { get; set; }
    }
}
