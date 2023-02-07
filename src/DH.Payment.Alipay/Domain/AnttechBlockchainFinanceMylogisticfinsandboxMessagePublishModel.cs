using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AnttechBlockchainFinanceMylogisticfinsandboxMessagePublishModel Data Structure.
    /// </summary>
    public class AnttechBlockchainFinanceMylogisticfinsandboxMessagePublishModel : AlipayObject
    {
        /// <summary>
        /// 路由方法的参数
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        /// 方法路由
        /// </summary>
        [JsonPropertyName("method_name")]
        public string MethodName { get; set; }
    }
}
