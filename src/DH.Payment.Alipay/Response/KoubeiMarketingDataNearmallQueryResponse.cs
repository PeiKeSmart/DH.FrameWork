using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingDataNearmallQueryResponse.
    /// </summary>
    public class KoubeiMarketingDataNearmallQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 商圈信息
        /// </summary>
        [JsonPropertyName("near_mall_bos")]
        public List<NearMallBo> NearMallBos { get; set; }
    }
}
