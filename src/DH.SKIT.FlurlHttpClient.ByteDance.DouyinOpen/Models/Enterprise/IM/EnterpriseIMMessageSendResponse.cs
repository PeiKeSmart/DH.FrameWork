using System;

namespace SKIT.FlurlHttpClient.ByteDance.DouyinOpen.Models
{
    /// <summary>
    /// <para>表示 [POST] /enterprise/im/message/send 接口的响应。</para>
    /// </summary>
    [Obsolete("相关接口或字段于 2023-05-31 下线。")]
    public class EnterpriseIMMessageSendResponse : DouyinOpenResponse<EnterpriseIMMessageSendResponse.Types.Data>
    {
        public static class Types
        {
            public class Data : DouyinOpenResponseData
            {
                /// <summary>
                /// 获取或设置服务端消息标识。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("server_msg_id")]
                [System.Text.Json.Serialization.JsonPropertyName("server_msg_id")]
                public long? ServerMessageId { get; set; }
            }
        }
    }
}
