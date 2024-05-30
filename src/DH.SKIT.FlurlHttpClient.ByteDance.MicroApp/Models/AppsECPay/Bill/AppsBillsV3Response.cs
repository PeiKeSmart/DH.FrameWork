namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /api/apps/v3/bills/ 接口的响应。</para>
    /// </summary>
    public class AppsBillsV3Response : DouyinMicroAppResponse
    {
        public static class Types
        {
            public class Data
            {
                /// <summary>
                /// 获取或设置账单下载链接列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("bill_list")]
                [System.Text.Json.Serialization.JsonPropertyName("bill_list")]
                public string[] DownloadUrlList { get; set; } = default!;
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
