using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenAppServiceMiniappnearbyQueryModel Data Structure.
    /// </summary>
    public class AlipayOpenAppServiceMiniappnearbyQueryModel : AlipayObject
    {
        /// <summary>
        /// 服务编码
        /// </summary>
        [JsonPropertyName("service_code")]
        public string ServiceCode { get; set; }
    }
}
