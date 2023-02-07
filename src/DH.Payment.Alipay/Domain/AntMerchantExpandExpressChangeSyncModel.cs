using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AntMerchantExpandExpressChangeSyncModel Data Structure.
    /// </summary>
    public class AntMerchantExpandExpressChangeSyncModel : AlipayObject
    {
        /// <summary>
        /// 传入需要上传的附件内容及相关业务参数
        /// </summary>
        [JsonPropertyName("asset_logistics_record")]
        public AssetLogisticsRecord AssetLogisticsRecord { get; set; }
    }
}
