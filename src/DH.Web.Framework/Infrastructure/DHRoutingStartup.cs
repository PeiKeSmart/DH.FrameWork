using DH.Core.Infrastructure;
using DH.Web.Framework.Infrastructure.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.Web.Framework.Infrastructure
{
    /// <summary>
    /// 表示应用程序启动时配置路由的对象
    /// </summary>
    public partial class DHRoutingStartup : IDHStartup
    {
        /// <summary>
        /// 添加并配置任何中间件
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <param name="configuration">应用程序的配置</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 添加MiniProfiler服务
            services.AddDHMiniProfiler();
        }

        /// <summary>
        /// 配置添加的中间件的使用
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public void Configure(IApplicationBuilder application)
        {
            // 使用MiniProfiler必须在UseNopEndpoints之前
            application.UseMiniProfiler();

            // 添加RoutingMiddleware
            application.UseRouting();
        }

        /// <summary>
        /// 获取此启动配置实现的顺序
        /// </summary>
        public int Order => 400; // 应在身份验证之前加载路由
    }
}
