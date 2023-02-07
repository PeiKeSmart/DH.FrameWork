using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateCampuscardAuthorizedQueryResponse.
    /// </summary>
    public class AlipayCommerceEducateCampuscardAuthorizedQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 学生的校园卡列表
        /// </summary>
        [JsonPropertyName("alipay_card_simple_list")]
        public List<SchoolCardSimpleInfo> AlipayCardSimpleList { get; set; }
    }
}
