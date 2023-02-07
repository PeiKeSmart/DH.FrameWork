using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// ScenicAuditResponse Data Structure.
    /// </summary>
    public class ScenicAuditResponse : AlipayObject
    {
        /// <summary>
        /// 景区审核信息查询结果
        /// </summary>
        [JsonPropertyName("scenic_audit_info")]
        public List<ScenicAuditInfo> ScenicAuditInfo { get; set; }
    }
}
