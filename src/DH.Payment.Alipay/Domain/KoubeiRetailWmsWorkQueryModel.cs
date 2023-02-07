using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiRetailWmsWorkQueryModel Data Structure.
    /// </summary>
    public class KoubeiRetailWmsWorkQueryModel : AlipayObject
    {
        /// <summary>
        /// 操作上下文
        /// </summary>
        [JsonPropertyName("operate_context")]
        public OperateContext OperateContext { get; set; }

        /// <summary>
        /// 作业id
        /// </summary>
        [JsonPropertyName("work_id")]
        public string WorkId { get; set; }
    }
}
