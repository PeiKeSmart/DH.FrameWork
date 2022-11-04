namespace DH.FlurlHttpClient.KeRuYun.Api
{
    /// <summary>
    /// 一个用于构造 <see cref="KeRuYunApiClient"/> 时使用的配置项。
    /// </summary>
    public class KeRuYunApiClientOptions
    {
        /// <summary>
        /// 获取或设置请求超时时间（单位：毫秒）。
        /// <para>默认值：30000</para>
        /// </summary>
        public int Timeout { get; set; } = 30 * 1000;

        /// <summary>
        /// 获取或设置银豹 API 域名。
        /// <para>默认值：<see cref="KeRuYunApiEndpoints.DEFAULT"/></para>
        /// </summary>
        public string Endpoints { get; set; } = KeRuYunApiEndpoints.DEFAULT;

        /// <summary>
        /// 获取或设置客如云 AppKey。
        /// </summary>
        public string AppKey { get; set; } = default!;

        /// <summary>
        /// 获取或设置客如云 APPSecret。
        /// </summary>
        public string APPSecret { get; set; } = default!;

        /// <summary>
        /// 版本,默认2.0
        /// </summary>
        public String Version { get; set; } = "2.0";
    }
}
