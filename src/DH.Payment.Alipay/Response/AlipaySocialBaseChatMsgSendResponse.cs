﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySocialBaseChatMsgSendResponse.
    /// </summary>
    public class AlipaySocialBaseChatMsgSendResponse : AlipayResponse
    {
        /// <summary>
        /// 消息索引号 会话ID_消息ID
        /// </summary>
        [JsonPropertyName("msg_index")]
        public string MsgIndex { get; set; }
    }
}
