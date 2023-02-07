using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayPcreditLoanRepayDetailQueryResponse.
    /// </summary>
    public class AlipayPcreditLoanRepayDetailQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 还款流水明细列表
        /// </summary>
        [JsonPropertyName("repay_detail_list")]
        public List<RepayDetailVO> RepayDetailList { get; set; }

        /// <summary>
        /// 结果总数
        /// </summary>
        [JsonPropertyName("total")]
        public long Total { get; set; }
    }
}
