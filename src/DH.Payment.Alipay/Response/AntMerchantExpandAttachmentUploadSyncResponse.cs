using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandAttachmentUploadSyncResponse.
    /// </summary>
    public class AntMerchantExpandAttachmentUploadSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 响应结果
        /// </summary>
        [JsonPropertyName("upload_result")]
        public AssetResult UploadResult { get; set; }
    }
}
