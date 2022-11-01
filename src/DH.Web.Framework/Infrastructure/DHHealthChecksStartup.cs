using DH.Core.Infrastructure;
using DH.VirtualFileSystem;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.Web.Framework.Infrastructure
{
    /// <summary>
    /// 表示应用程序启动时配置服务的对象
    /// </summary>
    public partial class DHHealthStartup : IDHStartup
    {
        /// <summary>
        /// 配置添加的中间件的使用
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks("/health");
        }

        /// <summary>
        /// 添加并配置任何中间件
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <param name="configuration">应用程序的配置</param>
        /// <param name="startups">查找到的IDHStartup集合</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnumerable<IDHStartup> startups)
        {
            services.AddHealthChecks();  // 健康检查，可以增加自定义方法。
        }

        /// <summary>
        /// 配置虚拟文件系统
        /// </summary>
        /// <param name="options">虚拟文件配置</param>
        public void ConfigureVirtualFileSystem(DHVirtualFileSystemOptions options)
        {
        }

        /// <summary>
        /// 获取此启动配置实现的顺序
        /// </summary>
        public int Order => 50;  // 对程序健康检测应在比较靠前的地方注册
    }
}
