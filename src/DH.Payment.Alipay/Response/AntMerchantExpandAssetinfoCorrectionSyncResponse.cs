using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandAssetinfoCorrectionSyncResponse.
    /// </summary>
    public class AntMerchantExpandAssetinfoCorrectionSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 物料更正请求响应。
        /// </summary>
        [JsonPropertyName("correction_results")]
        public List<AssetResult> CorrectionResults { get; set; }
    }
}
