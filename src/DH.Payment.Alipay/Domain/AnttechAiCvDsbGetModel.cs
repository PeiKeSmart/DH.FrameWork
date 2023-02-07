using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AnttechAiCvDsbGetModel Data Structure.
    /// </summary>
    public class AnttechAiCvDsbGetModel : AlipayObject
    {
        /// <summary>
        /// 定损任务接口返回的task_id
        /// </summary>
        [JsonPropertyName("task_id")]
        public string TaskId { get; set; }
    }
}
