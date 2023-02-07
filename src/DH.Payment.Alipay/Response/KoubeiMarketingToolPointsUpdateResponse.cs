using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingToolPointsUpdateResponse.
    /// </summary>
    public class KoubeiMarketingToolPointsUpdateResponse : AlipayResponse
    {
        /// <summary>
        /// 集点变更流水号
        /// </summary>
        [JsonPropertyName("point_log_no")]
        public string PointLogNo { get; set; }
    }
}
