namespace DG.FlurlHttpClient.Pospal.Api
{
    /// <summary>
    /// 一个用于构造 <see cref="PospalApiClient"/> 时使用的配置项。
    /// </summary>
    public class PospalApiClientOptions
    {
        /// <summary>
        /// 获取或设置请求超时时间（单位：毫秒）。
        /// <para>默认值：30000</para>
        /// </summary>
        public int Timeout { get; set; } = 30 * 1000;

        /// <summary>
        /// 获取或设置银豹 API 域名。
        /// <para>默认值：<see cref="PospalApiEndpoints.DEFAULT"/></para>
        /// </summary>
        public string Endpoints { get; set; } = PospalApiEndpoints.DEFAULT;

        /// <summary>
        /// 获取或设置银豹 AppId。
        /// </summary>
        public string AppId { get; set; } = default!;

        /// <summary>
        /// 获取或设置银豹 AppKey。
        /// </summary>
        public string AppKey { get; set; } = default!;
    }
}
