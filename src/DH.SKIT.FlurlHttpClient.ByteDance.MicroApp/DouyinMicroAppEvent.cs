namespace SKIT.FlurlHttpClient.ByteDance.MicroApp
{
    /// <summary>
    /// 表示抖音小程序 API 消息推送事件的基类。
    /// </summary>
    [Newtonsoft.Json.JsonObject]
    [System.Xml.Serialization.XmlRoot("xml")]
    public class DouyinMicroAppEvent : ICommonWebhookEvent
    {
        /// <summary>
        /// 获取或设置消息接收方账号。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("ToUserName")]
        [System.Text.Json.Serialization.JsonPropertyName("ToUserName")]
        [System.Xml.Serialization.XmlElement("ToUserName", IsNullable = true)]
        public virtual string? ToUserName { get; set; }

        /// <summary>
        /// 获取或设置消息发送方账号。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("FromUserName")]
        [System.Text.Json.Serialization.JsonPropertyName("FromUserName")]
        [System.Xml.Serialization.XmlElement("FromUserName", IsNullable = true)]
        public virtual string? FromUserName { get; set; }

        /// <summary>
        /// 获取或设置消息类型。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("MsgType")]
        [System.Text.Json.Serialization.JsonPropertyName("MsgType")]
        [System.Xml.Serialization.XmlElement("MsgType", IsNullable = true)]
        public virtual string? MessageType { get; set; }

        /// <summary>
        /// 获取或设置消息创建时间戳。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("CreateTime")]
        [System.Text.Json.Serialization.JsonPropertyName("CreateTime")]
        [System.Text.Json.Serialization.JsonNumberHandling(System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString)]
        [System.Xml.Serialization.XmlElement("CreateTime")]
        public virtual long CreateTimestamp { get; set; }
    }
}
