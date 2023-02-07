using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringPosSpecQueryResponse.
    /// </summary>
    public class KoubeiCateringPosSpecQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 规格列表
        /// </summary>
        [JsonPropertyName("specs")]
        public List<SpecEntity> Specs { get; set; }
    }
}
