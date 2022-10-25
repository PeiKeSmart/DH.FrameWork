using DH.Core.Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.Web.Framework.Infrastructure
{
    /// <summary>
    /// 表示应用程序启动时配置授权中间件的对象
    /// </summary>
    public partial class AuthorizationStartup : IDHStartup
    {
        /// <summary>
        /// 添加并配置任何中间件
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <param name="configuration">应用程序的配置</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        /// <summary>
        /// 配置添加的中间件的使用
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public void Configure(IApplicationBuilder application)
        {
            // 添加授权中间件
            application.UseAuthorization();
        }

        /// <summary>
        /// 获取此启动配置实现的顺序
        /// </summary>
        public int Order => 600; // 应在端点之前和身份验证之后加载授权
    }
}
