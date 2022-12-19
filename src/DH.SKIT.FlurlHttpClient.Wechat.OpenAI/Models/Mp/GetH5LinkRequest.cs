namespace SKIT.FlurlHttpClient.Wechat.OpenAI.Models
{
    /// <summary>
    /// <para>表示 [POST] /geth5link/{TOKEN} 接口的请求。</para>
    /// </summary>
    public class GetH5LinkRequest : WechatOpenAIRequest, WechatOpenAIRequest.Serialization.IEncryptedXmlable
    {
        /// <summary>
        /// 获取或设置页面标题。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("title")]
        [System.Text.Json.Serialization.JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}
