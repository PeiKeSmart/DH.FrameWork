﻿namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /openapi/v1/microapp/app/modify_app_intro 接口的请求。</para>
    /// </summary>
    public class OpenApiMicroAppAppModifyAppIntroductionV1Request : ByteDanceMicroAppRequest
    {
        /// <summary>
        /// 获取或设置第三方应用 AppId。如果不指定将使用构造 <see cref="ByteDanceMicroAppClient"/> 时的 <see cref="ByteDanceMicroAppClientOptions.AppId"/> 参数。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? ComponentAppId { get; set; }

        /// <summary>
        /// 获取或设置准备修改的小程序简介。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("new_intro")]
        [System.Text.Json.Serialization.JsonPropertyName("new_intro")]
        public string NewIntroduction { get; set; } = string.Empty;
    }
}
