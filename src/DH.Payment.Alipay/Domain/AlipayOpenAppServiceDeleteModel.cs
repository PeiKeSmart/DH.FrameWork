using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenAppServiceDeleteModel Data Structure.
    /// </summary>
    public class AlipayOpenAppServiceDeleteModel : AlipayObject
    {
        /// <summary>
        /// 服务id
        /// </summary>
        [JsonPropertyName("service_code")]
        public string ServiceCode { get; set; }
    }
}
