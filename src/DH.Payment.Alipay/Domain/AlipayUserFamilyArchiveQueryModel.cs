using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayUserFamilyArchiveQueryModel Data Structure.
    /// </summary>
    public class AlipayUserFamilyArchiveQueryModel : AlipayObject
    {
        /// <summary>
        /// 家人信息档案(选人授权)组件渲染请求令牌
        /// </summary>
        [JsonPropertyName("archive_token")]
        public string ArchiveToken { get; set; }
    }
}
