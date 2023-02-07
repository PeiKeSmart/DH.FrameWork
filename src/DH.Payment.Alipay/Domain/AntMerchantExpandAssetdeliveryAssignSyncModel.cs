using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AntMerchantExpandAssetdeliveryAssignSyncModel Data Structure.
    /// </summary>
    public class AntMerchantExpandAssetdeliveryAssignSyncModel : AlipayObject
    {
        /// <summary>
        /// 配送指令接收反馈，最多200条
        /// </summary>
        [JsonPropertyName("delivery_results")]
        public List<AssetResult> DeliveryResults { get; set; }
    }
}
