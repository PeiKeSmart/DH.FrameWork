using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandDeliveryProcessSyncResponse.
    /// </summary>
    public class AntMerchantExpandDeliveryProcessSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 响应参数
        /// </summary>
        [JsonPropertyName("result")]
        public AssetResult Result { get; set; }
    }
}
