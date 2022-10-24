using DH.Core.Caching;
using DH.Entity;

namespace DH.Services.Security
{
    /// <summary>
    /// 表示与安全服务相关的默认值
    /// </summary>
    public static partial class DHSecurityDefaults
    {
        #region reCAPTCHA

        /// <summary>
        /// 获取reCAPTCHA脚本URL
        /// </summary>
        /// <remarks>
        /// {0} : 页面上的recaptcha实例的Id
        /// {1} : 渲染类型
        /// {2} : 语言（如果存在）
        /// </remarks>
        public static string RecaptchaScriptPath => "api.js?onload=onloadCallback{0}&render={1}{2}";

        /// <summary>
        /// 获取reCAPTCHA验证URL
        /// </summary>
        /// <remarks>
        /// {0} : 私钥
        /// {1} : 响应值
        /// {2} : IP地址
        /// </remarks>
        public static string RecaptchaValidationPath => "api/siteverify?secret={0}&response={1}&remoteip={2}";

        #endregion
    }
}
