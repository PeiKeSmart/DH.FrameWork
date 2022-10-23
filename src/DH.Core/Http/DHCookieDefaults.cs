namespace DH.Core.Http
{
    /// <summary>
    /// 表示与cookie相关的默认值
    /// </summary>
    public static partial class DHCookieDefaults
    {
        /// <summary>
        /// 获取cookie名称前缀
        /// </summary>
        public static string Prefix => ".DH";

        /// <summary>
        /// 获取用户的cookie名称
        /// </summary>
        public static string CustomerCookie => ".Customer";

        /// <summary>
        /// 获取反伪造的cookie名称
        /// </summary>
        public static string AntiforgeryCookie => ".Antiforgery";

        /// <summary>
        /// 获取会话状态的cookie名称
        /// </summary>
        public static string SessionCookie => ".Session";

        /// <summary>
        /// 获取区域性的cookie名称
        /// </summary>
        public static string CultureCookie => ".Culture";

        /// <summary>
        /// 获取临时数据的cookie名称
        /// </summary>
        public static string TempDataCookie => ".TempData";

        /// <summary>
        /// 获取安装语言的cookie名称
        /// </summary>
        public static string InstallationLanguageCookie => ".InstallationLanguage";

        /// <summary>
        /// 获取比较产品的cookie名称
        /// </summary>
        public static string ComparedProductsCookie => ".ComparedProducts";

        /// <summary>
        /// 获取最近查看的产品的cookie名称
        /// </summary>
        public static string RecentlyViewedProductsCookie => ".RecentlyViewedProducts";

        /// <summary>
        /// 获取身份验证的cookie名称
        /// </summary>
        public static string AuthenticationCookie => ".Authentication";

        /// <summary>
        /// 获取外部身份验证的cookie名称
        /// </summary>
        public static string ExternalAuthenticationCookie => ".ExternalAuthentication";

        /// <summary>
        /// 获取Eu cookie法律警告的cookie名称
        /// </summary>
        public static string IgnoreEuCookieLawWarning => ".IgnoreEuCookieLawWarning";
    }
}
