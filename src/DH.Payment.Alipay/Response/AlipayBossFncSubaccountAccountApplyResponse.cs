using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossFncSubaccountAccountApplyResponse.
    /// </summary>
    public class AlipayBossFncSubaccountAccountApplyResponse : AlipayResponse
    {
        /// <summary>
        /// 申请子户结果
        /// </summary>
        [JsonPropertyName("result_set")]
        public ApplySubAccountAndBindResultDTO ResultSet { get; set; }
    }
}
