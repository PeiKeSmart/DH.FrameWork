namespace DH.FlurlHttpClient.KeRuYun.Api.Settings
{
    public class Credentials
    {
        /// <summary>
        /// 初始化客户端时 <see cref="KeRuYunApiClientOptions.AppKey"/> 的副本。
        /// </summary>
        public string AppKey { get; }

        /// <summary>
        /// 初始化客户端时 <see cref="KeRuYunApiClientOptions.APPSecret"/> 的副本。
        /// </summary>
        public string APPSecret { get; }

        /// <summary>
        /// 初始化客户端时 <see cref="KeRuYunApiClientOptions.Version"/> 的副本。
        /// </summary>
        public String Version { get;}

        internal Credentials(KeRuYunApiClientOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            APPSecret = options.AppSecret;
            AppKey = options.AppKey;
            Version = options.Version;
        }
    }
}
