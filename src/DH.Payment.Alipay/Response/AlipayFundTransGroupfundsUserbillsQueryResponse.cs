using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayFundTransGroupfundsUserbillsQueryResponse.
    /// </summary>
    public class AlipayFundTransGroupfundsUserbillsQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 当前用户涉及的流水模型列表，仅返回实际存在的流水数据
        /// </summary>
        [JsonPropertyName("user_bills")]
        public GroupFundUserBill UserBills { get; set; }
    }
}
