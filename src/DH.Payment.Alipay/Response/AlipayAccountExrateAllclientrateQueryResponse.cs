using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayAccountExrateAllclientrateQueryResponse.
    /// </summary>
    public class AlipayAccountExrateAllclientrateQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 汇率信息列表
        /// </summary>
        [JsonPropertyName("client_rate_list")]
        public List<ExClientRateVO> ClientRateList { get; set; }
    }
}
