using DH.Core.Infrastructure;
using DH.Web.Framework.Infrastructure.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.Web.Framework.Infrastructure
{
    /// <summary>
    /// 表示应用程序启动时配置路由的类
    /// </summary>
    public partial class DHStaticFilesStartup : IDHStartup
    {
        /// <summary>
        /// 添加并配置任何中间件
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <param name="configuration">应用程序的配置</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 压缩
            services.AddResponseCompression();

            // 用于绑定和缩小CSS和JavaScript文件的中间件。
            services.AddDHWebOptimizer();
        }

        /// <summary>
        /// 配置添加的中间件的使用
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public void Configure(IApplicationBuilder application)
        {
            // 在UseDHStaticFiles之前使用响应压缩以支持对其进行压缩
            application.UseDHResponseCompression();

            // 配置静态文件之前应放置WebOptimizer
            application.UseDHWebOptimizer();

            // 使用静态文件功能
            application.UseDHStaticFiles();
        }

        /// <summary>
        /// 获取此启动配置实现的顺序
        /// </summary>
        public int Order => 99; // 静态文件应在路由和自定义中间件之前注册
    }
}
