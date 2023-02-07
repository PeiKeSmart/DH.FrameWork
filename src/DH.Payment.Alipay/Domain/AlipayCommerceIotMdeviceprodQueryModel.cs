using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceIotMdeviceprodQueryModel Data Structure.
    /// </summary>
    public class AlipayCommerceIotMdeviceprodQueryModel : AlipayObject
    {
        /// <summary>
        /// 设备id（物料系统的id）
        /// </summary>
        [JsonPropertyName("asset_id")]
        public string AssetId { get; set; }
    }
}
