using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenMiniAmpeDevicetypeBatchqueryModel Data Structure.
    /// </summary>
    public class AlipayOpenMiniAmpeDevicetypeBatchqueryModel : AlipayObject
    {
        /// <summary>
        /// 场景码，申请后平台分配，必填
        /// </summary>
        [JsonPropertyName("scene_code")]
        public string SceneCode { get; set; }
    }
}
