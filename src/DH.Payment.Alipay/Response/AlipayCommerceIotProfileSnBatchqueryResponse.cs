using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceIotProfileSnBatchqueryResponse.
    /// </summary>
    public class AlipayCommerceIotProfileSnBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// sn列表
        /// </summary>
        [JsonPropertyName("sn_list")]
        public List<ProfileSnDetail> SnList { get; set; }
    }
}
