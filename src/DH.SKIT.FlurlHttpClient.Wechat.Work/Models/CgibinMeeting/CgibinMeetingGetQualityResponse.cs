namespace SKIT.FlurlHttpClient.Wechat.Work.Models
{
    /// <summary>
    /// <para>表示 [POST] /cgi-bin/meeting/get_quality 接口的响应。</para>
    /// </summary>
    public class CgibinMeetingGetQualityResponse : WechatWorkResponse
    {
        public static class Types
        {
            public class Attendee
            {
                /// <summary>
                /// 获取或设置参与者成员账号。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("userid")]
                [System.Text.Json.Serialization.JsonPropertyName("userid")]
                public string UserId { get; set; } = default!;

                /// <summary>
                /// 获取或设置参与者临时 OpenId。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("tmp_openid")]
                [System.Text.Json.Serialization.JsonPropertyName("tmp_openid")]
                public string? TempOpenId { get; set; }

                /// <summary>
                /// 获取或设置终端设备类型。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("instance_id")]
                [System.Text.Json.Serialization.JsonPropertyName("instance_id")]
                public int InstanceId { get; set; }

                /// <summary>
                /// 获取或设置健康度。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("quality")]
                [System.Text.Json.Serialization.JsonPropertyName("quality")]
                public int Quality { get; set; }

                /// <summary>
                /// 获取或设置音频质量。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("audio_quality")]
                [System.Text.Json.Serialization.JsonPropertyName("audio_quality")]
                public int AudioQuality { get; set; }

                /// <summary>
                /// 获取或设置视频质量。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("video_quality")]
                [System.Text.Json.Serialization.JsonPropertyName("video_quality")]
                public int VideoQuality { get; set; }

                /// <summary>
                /// 获取或设置共享屏幕质量。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("screen_share_quality")]
                [System.Text.Json.Serialization.JsonPropertyName("screen_share_quality")]
                public int ScreenShareQuality { get; set; }

                /// <summary>
                /// 获取或设置网络质量。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("network_quality")]
                [System.Text.Json.Serialization.JsonPropertyName("network_quality")]
                public int NetworkQuality { get; set; }

                /// <summary>
                /// 获取或设置告警的具体问题列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("problems")]
                [System.Text.Json.Serialization.JsonPropertyName("problems")]
                public string[]? ProblemList { get; set; }
            }
        }

        /// <summary>
        /// 获取或设置健康度。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("quality")]
        [System.Text.Json.Serialization.JsonPropertyName("quality")]
        public int Quality { get; set; }

        /// <summary>
        /// 获取或设置音频质量。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("audio_quality")]
        [System.Text.Json.Serialization.JsonPropertyName("audio_quality")]
        public int AudioQuality { get; set; }

        /// <summary>
        /// 获取或设置视频质量。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("video_quality")]
        [System.Text.Json.Serialization.JsonPropertyName("video_quality")]
        public int VideoQuality { get; set; }

        /// <summary>
        /// 获取或设置共享屏幕质量。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("screen_share_quality")]
        [System.Text.Json.Serialization.JsonPropertyName("screen_share_quality")]
        public int ScreenShareQuality { get; set; }

        /// <summary>
        /// 获取或设置网络质量。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("network_quality")]
        [System.Text.Json.Serialization.JsonPropertyName("network_quality")]
        public int NetworkQuality { get; set; }

        /// <summary>
        /// 获取或设置告警的具体问题列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("problems")]
        [System.Text.Json.Serialization.JsonPropertyName("problems")]
        public string[]? ProblemList { get; set; }

        /// <summary>
        /// 获取或设置参会人员列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("attendees")]
        [System.Text.Json.Serialization.JsonPropertyName("attendees")]
        public Types.Attendee[] AttendeeList { get; set; } = default!;

        /// <summary>
        /// 获取或设置是否还有更多。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("has_more")]
        [System.Text.Json.Serialization.JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

        /// <summary>
        /// 获取或设置翻页标记。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("next_cursor")]
        [System.Text.Json.Serialization.JsonPropertyName("next_cursor")]
        public string? NextCursor { get; set; }
    }
}
