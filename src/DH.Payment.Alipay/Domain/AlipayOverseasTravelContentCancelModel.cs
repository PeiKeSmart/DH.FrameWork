using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOverseasTravelContentCancelModel Data Structure.
    /// </summary>
    public class AlipayOverseasTravelContentCancelModel : AlipayObject
    {
        /// <summary>
        /// 内容id
        /// </summary>
        [JsonPropertyName("content_id")]
        public string ContentId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [JsonPropertyName("modified_date")]
        public long ModifiedDate { get; set; }
    }
}
