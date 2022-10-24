using Microsoft.AspNetCore.Authentication;

namespace DH.Services.Authentication.External
{
    /// <summary>
    /// 用于注册（配置）外部身份验证服务（插件）的接口
    /// </summary>
    public interface IExternalAuthenticationRegistrar
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder">身份验证生成器</param>
        void Configure(AuthenticationBuilder builder);
    }
}
