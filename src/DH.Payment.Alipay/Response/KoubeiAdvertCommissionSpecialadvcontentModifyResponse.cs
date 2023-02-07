using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiAdvertCommissionSpecialadvcontentModifyResponse.
    /// </summary>
    public class KoubeiAdvertCommissionSpecialadvcontentModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 修改特殊广告内容的返回结果
        /// </summary>
        [JsonPropertyName("data")]
        public List<KbAdvertSpecialAdvContentModifyResponse> Data { get; set; }
    }
}
