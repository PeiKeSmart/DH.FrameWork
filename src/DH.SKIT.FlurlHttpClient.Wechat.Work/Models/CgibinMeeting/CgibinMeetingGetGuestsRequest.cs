namespace SKIT.FlurlHttpClient.Wechat.Work.Models
{
    /// <summary>
    /// <para>表示 [POST] /cgi-bin/meeting/get_guests 接口的请求。</para>
    /// </summary>
    public class CgibinMeetingGetGuestsRequest : WechatWorkRequest
    {
        /// <summary>
        /// 获取或设置会议 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("meetingid")]
        [System.Text.Json.Serialization.JsonPropertyName("meetingid")]
        public string MeetingId { get; set; } = string.Empty;
    }
}
