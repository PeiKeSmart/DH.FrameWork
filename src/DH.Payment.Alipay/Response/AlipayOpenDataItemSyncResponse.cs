using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenDataItemSyncResponse.
    /// </summary>
    public class AlipayOpenDataItemSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 返回更新成功的外部id
        /// </summary>
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }
    }
}
