namespace DH.FlurlHttpClient.KeRuYun.Api
{
    /// <summary>
    /// <para>客如云 API 接口域名。</para>
    /// </summary>
    public static class KeRuYunApiEndpoints
    {
        /// <summary>
        /// 正式环境域名（默认）。
        /// </summary>
        public const string DEFAULT = "https://openapi.keruyun.com";

        /// <summary>
        /// Oauth授权域名。Oauth2只有Oauth授权接口才可用到该域名，其它接口统一使用正式环境域名
        /// </summary>
        public const string OAUTH2 = "https://open.keruyun.com";
    }
}
