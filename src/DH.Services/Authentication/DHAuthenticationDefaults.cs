using Microsoft.AspNetCore.Http;

namespace DH.Services.Authentication
{
    /// <summary>
    /// 表示与身份验证服务相关的默认值
    /// </summary>
    public static partial class DHAuthenticationDefaults
    {
        /// <summary>
        /// 用于身份验证方案的默认值
        /// </summary>
        public static string AuthenticationScheme => "Authentication";

        /// <summary>
        /// 用于外部身份验证方案的默认值
        /// </summary>
        public static string ExternalAuthenticationScheme => "ExternalAuthentication";

        /// <summary>
        /// 应用于创建的任何索赔的发行人
        /// </summary>
        public static string ClaimsIssuer => "DHSoft";

        /// <summary>
        /// 登录路径的默认值
        /// </summary>
        public static PathString LoginPath => new("/login");

        /// <summary>
        /// 拒绝访问路径的默认值
        /// </summary>
        public static PathString AccessDeniedPath => new("/page-not-found");

        /// <summary>
        /// 返回URL参数的默认值
        /// </summary>
        public static string ReturnUrlParameter => string.Empty;

        /// <summary>
        /// 获取用于将外部身份验证错误存储到会话的密钥
        /// </summary>
        public static string ExternalAuthenticationErrorsSessionKey => "dh.externalauth.errors";
    }
}
