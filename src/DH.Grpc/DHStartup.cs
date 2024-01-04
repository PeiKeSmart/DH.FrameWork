using DH.Core.Infrastructure;
using DH.VirtualFileSystem;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.Grpc;

/// <summary>
/// 表示应用程序启动时配置Grpc的对象
/// </summary>
public class DHStartup : IDHStartup
{
    /// <summary>
    /// 配置添加的中间件的使用
    /// </summary>
    /// <param name="application">用于配置应用程序的请求管道的生成器</param>
    public void Configure(IApplicationBuilder application)
    {
        application.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });  // 必须在UseRouting和UseEndpoints之间添加  GrpcWebOptions 全局所有服务在默认情况下都支持gRPC-Web
    }

    /// <summary>
    /// 添加并配置任何中间件
    /// </summary>
    /// <param name="services">服务描述符集合</param>
    /// <param name="configuration">应用程序的配置</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        services.AddGrpc(options =>
        {
            options.Interceptors.Add<GRPCInterceptor>();
            options.EnableDetailedErrors = true;
        });
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
    public int Order => 750;
}
