using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniInnerdeploypackageQueryResponse.
    /// </summary>
    public class AlipayOpenMiniInnerdeploypackageQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 小程序部署包模型
        /// </summary>
        [JsonPropertyName("app_deploy_package")]
        public OpenAppDeployPackageVO AppDeployPackage { get; set; }
    }
}
