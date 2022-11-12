﻿using DH.Core.Infrastructure;
using DH.VirtualFileSystem;
using DH.Web.Framework.Infrastructure.Extensions;
using DH.Web.Framework.Mvc.Routing;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.Web.Framework.Infrastructure
{
    /// <summary>
    /// 表示应用程序启动时配置终结点的对象
    /// </summary>
    public partial class DHEndpointsStartup : IDHStartup
    {
        /// <summary>
        /// 添加并配置任何中间件
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <param name="configuration">应用程序的配置</param>
        /// <param name="startups">查找到的IDHStartup集合</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnumerable<IDHStartup> startups)
        {
            services.AddSingleton<TranslationTransformer>();
        }

        /// <summary>
        /// 配置添加的中间件的使用
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public void Configure(IApplicationBuilder application)
        {
            // 端点路由
            application.UseDHEndpoints();
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
        public int Order => 900; // 身份验证应在MVC之前加载
    }
}