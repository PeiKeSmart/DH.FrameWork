using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayAssetPointPointprodBudgetlibReturnResponse.
    /// </summary>
    public class AlipayAssetPointPointprodBudgetlibReturnResponse : AlipayResponse
    {
        /// <summary>
        /// 预算库回收失败时的错误信息
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
