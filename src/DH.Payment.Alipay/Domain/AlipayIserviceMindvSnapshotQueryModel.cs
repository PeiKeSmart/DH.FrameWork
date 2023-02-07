using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayIserviceMindvSnapshotQueryModel Data Structure.
    /// </summary>
    public class AlipayIserviceMindvSnapshotQueryModel : AlipayObject
    {
        /// <summary>
        /// 问卷/试卷快照id信息，供系统检索快照内容
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
