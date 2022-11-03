﻿namespace SKIT.FlurlHttpClient.Wechat.Work.Models
{
    /// <summary>
    /// <para>表示 [POST] /cgi-bin/kf/sync_msg 接口的请求。</para>
    /// </summary>
    public class CgibinKfSyncMessageRequest : WechatWorkRequest
    {
        /// <summary>
        /// 获取或设置同步消息的 Token。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("token")]
        [System.Text.Json.Serialization.JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置翻页标记。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("cursor")]
        [System.Text.Json.Serialization.JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// 获取或设置翻页每页数量。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("limit")]
        [System.Text.Json.Serialization.JsonPropertyName("limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// 获取或设置语音消息类型。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("voice_format")]
        [System.Text.Json.Serialization.JsonPropertyName("voice_format")]
        public int? VoiceFormat { get; set; }
    }
}
