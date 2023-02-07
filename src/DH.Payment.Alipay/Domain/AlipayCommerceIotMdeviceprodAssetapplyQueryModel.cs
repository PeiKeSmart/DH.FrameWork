using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceIotMdeviceprodAssetapplyQueryModel Data Structure.
    /// </summary>
    public class AlipayCommerceIotMdeviceprodAssetapplyQueryModel : AlipayObject
    {
        /// <summary>
        /// 物料平台的申请单ID
        /// </summary>
        [JsonPropertyName("apply_order_id")]
        public string ApplyOrderId { get; set; }
    }
}
