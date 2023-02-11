namespace SKIT.FlurlHttpClient.Wechat.Api.Models
{
    /// <summary>
    /// <para>表示 [POST] /channels/ec/sharer/search_sharer 接口的响应。</para>
    /// </summary>
    public class ChannelsECSharerSearchSharerResponse : WechatApiResponse
    {
        /// <summary>
        /// 获取或设置分享员 OpenId。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("openid")]
        [System.Text.Json.Serialization.JsonPropertyName("openid")]
        public string OpenId { get; set; } = default!;

        /// <summary>
        /// 获取或设置分享员昵称。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nickname")]
        [System.Text.Json.Serialization.JsonPropertyName("nickname")]
        public string Nickname { get; set; } = default!;

        /// <summary>
        /// 获取或设置分享员类型。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("sharer_type")]
        [System.Text.Json.Serialization.JsonPropertyName("sharer_type")]
        public int SharerType { get; set; }

        /// <summary>
        /// 获取或设置绑定时间戳。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bind_time")]
        [System.Text.Json.Serialization.JsonPropertyName("bind_time")]
        public long BindTimestamp { get; set; }
    }
}
