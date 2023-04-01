namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /linkmic/query 接口的请求。</para>
    /// </summary>
    public class WebcastLinkMicQueryRequest : ByteDanceMicroAppRequest
    {
        /// <summary>
        /// 获取或设置小玩法的 AppId。如果不指定将使用构造 <see cref="ByteDanceMicroAppClient"/> 时的 <see cref="ByteDanceMicroAppClientOptions.AppId"/> 参数。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("app_id")]
        [System.Text.Json.Serialization.JsonPropertyName("app_id")]
        public string? AppId { get; set; }

        /// <summary>
        /// 获取或设置直播间 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("room_id")]
        [System.Text.Json.Serialization.JsonPropertyName("room_id")]
        public string RoomId { get; set; } = string.Empty;
    }
}
