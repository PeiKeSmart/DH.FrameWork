using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayAssetPointPointprodBudgetlibQueryModel Data Structure.
    /// </summary>
    public class AlipayAssetPointPointprodBudgetlibQueryModel : AlipayObject
    {
        /// <summary>
        /// 预算库编码
        /// </summary>
        [JsonPropertyName("budget_code")]
        public string BudgetCode { get; set; }
    }
}
