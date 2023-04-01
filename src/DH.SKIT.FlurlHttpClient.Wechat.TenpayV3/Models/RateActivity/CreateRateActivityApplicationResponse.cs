namespace SKIT.FlurlHttpClient.Wechat.TenpayV3.Models
{
    /// <summary>
    /// <para>表示 [POST] /rate-activity/applications 接口的响应。</para>
    /// </summary>
    public class CreateRateActivityApplicationResponse : WechatTenpayResponse
    {
        /// <summary>
        /// 获取或设置申请单编号。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("application_id")]
        [System.Text.Json.Serialization.JsonPropertyName("application_id")]
        public string ApplicationId { get; set; } = default!;
    }
}
