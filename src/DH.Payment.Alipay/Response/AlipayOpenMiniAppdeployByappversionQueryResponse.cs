using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniAppdeployByappversionQueryResponse.
    /// </summary>
    public class AlipayOpenMiniAppdeployByappversionQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 发布信息
        /// </summary>
        [JsonPropertyName("deploys")]
        public List<MiniAppDeployResponse> Deploys { get; set; }
    }
}
