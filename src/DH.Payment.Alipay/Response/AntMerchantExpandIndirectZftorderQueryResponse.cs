using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandIndirectZftorderQueryResponse.
    /// </summary>
    public class AntMerchantExpandIndirectZftorderQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 直付通二级商户进件申请单信息
        /// </summary>
        [JsonPropertyName("orders")]
        public List<ZftSubMerchantOrder> Orders { get; set; }
    }
}
