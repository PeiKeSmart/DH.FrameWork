namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.SDK.OpenApi.Models
{
    /// <summary>
    /// <para>表示 [POST] /v1/microapp/app/modify_app_intro 接口的请求。</para>
    /// </summary>
    public class OpenApiMicroAppAppModifyAppIntroductionV1Request : ByteDanceMicroAppOpenApiRequest
    {
        /// <summary>
        /// 获取或设置准备修改的小程序简介。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("new_intro")]
        [System.Text.Json.Serialization.JsonPropertyName("new_intro")]
        public string NewIntroduction { get; set; } = string.Empty;
    }
}
