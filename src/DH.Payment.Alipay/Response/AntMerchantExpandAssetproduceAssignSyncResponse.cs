using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandAssetproduceAssignSyncResponse.
    /// </summary>
    public class AntMerchantExpandAssetproduceAssignSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 供应商处理生产指令结果
        /// </summary>
        [JsonPropertyName("asset_results")]
        public List<AssetResult> AssetResults { get; set; }
    }
}
