using DH.Core.Infrastructure;
using DH.VirtualFileSystem;
using DH.Web.Framework.Infrastructure.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.IO.Compression;

namespace DH.Web.Framework.Infrastructure;

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
    /// <param name="startups">查找到的IDHStartup集合</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnumerable<IDHStartup> startups, IWebHostEnvironment webHostEnvironment)
    {
        // 压缩
        services.AddResponseCompression();  // 注入响应缓存
        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        }).Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        }).AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
            {
                "text/html; charset=utf-8",
                "application/xhtml+xml",
                "application/atom+xml",
                "image/svg+xml",
                "application/octet-stream"
            });
        });

        // 用于绑定和缩小CSS和JavaScript文件的中间件。
        services.AddDHWebOptimizer();
    }

    /// <summary>
    /// 配置添加的中间件的使用
    /// </summary>
    /// <param name="application">用于配置应用程序的请求管道的生成器</param>
    /// <param name="typeFinder">类型处理器</param>
    public void Configure(IApplicationBuilder application, ITypeFinder typeFinder)
    {
        // 在UseDHStaticFiles之前使用响应压缩以支持对其进行压缩
        application.UseDHResponseCompression();

        // 配置静态文件之前应放置WebOptimizer
        application.UseDHWebOptimizer();

        // 使用静态文件功能
        application.UseDHStaticFiles();
    }

    /// <summary>
    /// 配置虚拟文件系统
    /// </summary>
    /// <param name="options">虚拟文件配置</param>
    public void ConfigureVirtualFileSystem(DHVirtualFileSystemOptions options)
    {
    }

    /// <summary>
    /// 注册路由
    /// </summary>
    /// <param name="endpoints">路由生成器</param>
    public void UseDHEndpoints(IEndpointRouteBuilder endpoints)
    {
    }

    /// <summary>
    /// 将区域路由写入数据库
    /// </summary>
    public void ConfigureArea()
    {

    }

    /// <summary>
    /// 调整菜单
    /// </summary>
    public void ChangeMenu()
    {

    }

    /// <summary>
    /// 升级处理逻辑
    /// </summary>
    public void Update()
    {

    }

    /// <summary>
    /// 获取此启动配置实现的顺序
    /// </summary>
    public int Order => 99; // 静态文件应在路由和自定义中间件之前注册
}
