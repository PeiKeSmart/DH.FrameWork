using DH.Core.Infrastructure;
using DH.Web.Framework.Infrastructure.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.Web.Framework.Infrastructure
{
    /// <summary>
    /// 表示应用程序启动时配置MVC的对象
    /// </summary>
    public partial class DHMvcStartup : IDHStartup
    {
        /// <summary>
        /// 添加并配置任何中间件
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <param name="configuration">应用程序的配置</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 添加和配置MVC功能
            services.AddDHMvc();

            services.AddWebEncoders();

            // 添加自定义重定向结果执行器
            services.AddDHRedirectResultExecutor();
        }

        /// <summary>
        /// 配置添加的中间件的使用
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public void Configure(IApplicationBuilder application)
        {
        }

        /// <summary>
        /// 获取此启动配置实现的顺序
        /// </summary>
        public int Order => 1000; // MVC应最后加载
    }
}
