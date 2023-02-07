using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AnttechBlockchainDefinCustomerMemberQueryResponse.
    /// </summary>
    public class AnttechBlockchainDefinCustomerMemberQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 会员对象
        /// </summary>
        [JsonPropertyName("member_info_list")]
        public List<DefiCustMemberDTO> MemberInfoList { get; set; }
    }
}
