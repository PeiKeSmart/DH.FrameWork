namespace DG.FlurlHttpClient.Pospal.Api.Settings
{
    public class Credentials
    {
        /// <summary>
        /// 初始化客户端时 <see cref="PospalApiClientOptions.AppId"/> 的副本。
        /// </summary>
        public string AppId { get; }

        /// <summary>
        /// 初始化客户端时 <see cref="PospalApiClientOptions.AppKey"/> 的副本。
        /// </summary>
        public string AppKey { get; }

        internal Credentials(PospalApiClientOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            AppId = options.AppId;
            AppKey = options.AppKey;
        }
    }
}
