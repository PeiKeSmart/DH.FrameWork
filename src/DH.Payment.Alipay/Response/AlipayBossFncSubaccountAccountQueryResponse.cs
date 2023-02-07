using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossFncSubaccountAccountQueryResponse.
    /// </summary>
    public class AlipayBossFncSubaccountAccountQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 查询子户结果信息
        /// </summary>
        [JsonPropertyName("result_set")]
        public SubAccountInfoDTO ResultSet { get; set; }
    }
}
