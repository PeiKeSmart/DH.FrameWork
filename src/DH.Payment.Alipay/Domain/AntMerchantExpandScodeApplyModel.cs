using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AntMerchantExpandScodeApplyModel Data Structure.
    /// </summary>
    public class AntMerchantExpandScodeApplyModel : AlipayObject
    {
        /// <summary>
        /// apply_code_request，申请S码的请求参数
        /// </summary>
        [JsonPropertyName("apply_code_request")]
        public List<ApplyCodeRequest> ApplyCodeRequest { get; set; }
    }
}
