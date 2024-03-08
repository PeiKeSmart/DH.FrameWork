namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /match/v2/taskbox/update_task/ 接口的响应。</para>
    /// </summary>
    public class MatchTaskBoxUpdateTaskV2Response : DouyinMicroAppResponse
    {
        public static class Types
        {
            public class Data : MatchTaskBoxAddTaskV2Response.Types.Data
            {
            }
        }

        /// <summary>
        /// 获取或设置返回数据。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("data")]
        [System.Text.Json.Serialization.JsonPropertyName("data")]
        public Types.Data Data { get; set; } = default!;
    }
}
