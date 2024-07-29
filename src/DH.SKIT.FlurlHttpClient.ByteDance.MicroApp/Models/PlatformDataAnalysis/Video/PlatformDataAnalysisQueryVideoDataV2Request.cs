using System.Collections.Generic;

namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /api/platform/v2/data_analysis/query_video_data/ 接口的请求。</para>
    /// </summary>
    public class PlatformDataAnalysisQueryVideoDataV2Request : DouyinMicroAppRequest
    {
        /// <summary>
        /// 获取或设置开始时间戳。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("start_time")]
        [System.Text.Json.Serialization.JsonPropertyName("start_time")]
        public long StartTimestamp { get; set; }

        /// <summary>
        /// 获取或设置结束时间戳。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("end_time")]
        [System.Text.Json.Serialization.JsonPropertyName("end_time")]
        public long EndTimestamp { get; set; }

        /// <summary>
        /// 获取或设置宿主名称。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("host_name")]
        [System.Text.Json.Serialization.JsonPropertyName("host_name")]
        public string? HostName { get; set; }

        /// <summary>
        /// 获取或设置查询数据的挂载类型。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("query_bind_type")]
        [System.Text.Json.Serialization.JsonPropertyName("query_bind_type")]
        public int QueryBindType { get; set; }

        /// <summary>
        /// 获取或设置抖音号列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("aweme_short_id_list")]
        [System.Text.Json.Serialization.JsonPropertyName("aweme_short_id_list")]
        public IList<string>? AwemeShortIdList { get; set; }

        /// <summary>
        /// 获取或设置视频 ID 列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("item_id_list")]
        [System.Text.Json.Serialization.JsonPropertyName("item_id_list")]
        public IList<string>? ItemIdList { get; set; }

        /// <summary>
        /// 获取或设置加密的视频 ID 列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("open_item_id_list")]
        [System.Text.Json.Serialization.JsonPropertyName("open_item_id_list")]
        public IList<string>? OpenItemIdList { get; set; }
    }
}
