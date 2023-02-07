using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenAppServiceMiniappnearbypoiQueryResponse.
    /// </summary>
    public class AlipayOpenAppServiceMiniappnearbypoiQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 服务poi信息列表
        /// </summary>
        [JsonPropertyName("addresses")]
        public AddressDTO Addresses { get; set; }
    }
}
