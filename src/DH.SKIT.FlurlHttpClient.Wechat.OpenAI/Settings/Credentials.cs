using System;

namespace SKIT.FlurlHttpClient.Wechat.OpenAI.Settings
{
    public sealed class Credentials
    {
        /// <summary>
        /// 初始化客户端时 <see cref="WechatOpenAIClientOptions.AppId"/> / <see cref="WechatChatbotClientOptions.AppId"/> 的副本。
        /// </summary>
        public string AppId { get; }

        /// <summary>
        /// 初始化客户端时 <see cref="WechatOpenAIClientOptions.Token"/> / <see cref="WechatChatbotClientOptions.Token"/> 的副本。
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// 初始化客户端时 <see cref="WechatOpenAIClientOptions.EncodingAESKey"/> / <see cref="WechatChatbotClientOptions.EncodingAESKey"/> 的副本。
        /// </summary>
        public string EncodingAESKey { get; }

        internal Credentials(WechatOpenAIClientOptions options)
        {
            if (options is null) throw new ArgumentNullException(nameof(options));

            AppId = options.AppId;
            Token = options.Token;
            EncodingAESKey = options.EncodingAESKey;
        }

        internal Credentials(WechatChatbotClientOptions options)
        {
            if (options is null) throw new ArgumentNullException(nameof(options));

            AppId = options.AppId;
            Token = options.Token;
            EncodingAESKey = options.EncodingAESKey;
        }
    }
}
