using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AntMerchantExpandAssetinfoCorrectionSyncModel Data Structure.
    /// </summary>
    public class AntMerchantExpandAssetinfoCorrectionSyncModel : AlipayObject
    {
        /// <summary>
        /// 物料信息更正请求
        /// </summary>
        [JsonPropertyName("asset_correction")]
        public AssetInfoCorrectionItem AssetCorrection { get; set; }
    }
}
