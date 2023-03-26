namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /apps/v1/capacity/update_capcut_template_status 接口的请求。</para>
    /// </summary>
    public class AppsCapacityUpdateCapcutTemplateStatusV1Request : ByteDanceMicroAppRequest
    {
        /// <summary>
        /// 获取或设置模版 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("template_id")]
        [System.Text.Json.Serialization.JsonPropertyName("template_id")]
        public long TemplateId { get; set; }

        /// <summary>
        /// 获取或设置开关状态。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("template_status")]
        [System.Text.Json.Serialization.JsonPropertyName("template_status")]
        public int TemplateStatus { get; set; }
    }
}
