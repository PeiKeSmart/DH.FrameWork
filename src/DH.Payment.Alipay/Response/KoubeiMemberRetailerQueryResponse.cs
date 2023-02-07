using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMemberRetailerQueryResponse.
    /// </summary>
    public class KoubeiMemberRetailerQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 零售商信息列表
        /// </summary>
        [JsonPropertyName("retailer_list")]
        public List<Retailer> RetailerList { get; set; }
    }
}
