using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayTradeAccountFundcompositionQueryResponse.
    /// </summary>
    public class AlipayTradeAccountFundcompositionQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 专户账号
        /// </summary>
        [JsonPropertyName("account_no")]
        public string AccountNo { get; set; }

        /// <summary>
        /// 银行专户的转入资金明细
        /// </summary>
        [JsonPropertyName("card_amount_detail_list")]
        public List<CardAmountDetailVO> CardAmountDetailList { get; set; }
    }
}
