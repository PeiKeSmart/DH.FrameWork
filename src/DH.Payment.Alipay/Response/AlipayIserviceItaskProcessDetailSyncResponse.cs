using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayIserviceItaskProcessDetailSyncResponse.
    /// </summary>
    public class AlipayIserviceItaskProcessDetailSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 蚂蚁工单编号
        /// </summary>
        [JsonPropertyName("alipay_process_id")]
        public string AlipayProcessId { get; set; }
    }
}
