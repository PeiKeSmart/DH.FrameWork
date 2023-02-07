using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayAssetPointVoucherprodBenefittemplateModifyResponse.
    /// </summary>
    public class AlipayAssetPointVoucherprodBenefittemplateModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 修改后模版的过期时间
        /// </summary>
        [JsonPropertyName("publish_end_time")]
        public string PublishEndTime { get; set; }
    }
}
