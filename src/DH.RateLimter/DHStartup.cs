using DH.Core.Infrastructure;
using DH.Helpers;
using DH.Helpers.Internal;
using DH.Models;
using DH.VirtualFileSystem;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.RateLimter;

/// <summary>
/// 表示应用程序启动时配置限流的对象
/// </summary>
public class DHStartup : IDHStartup
{
    /// <summary>
    /// 配置添加的中间件的使用
    /// </summary>
    /// <param name="application">用于配置应用程序的请求管道的生成器</param>
    /// <param name="typeFinder">类型处理器</param>
    public void Configure(IApplicationBuilder application, ITypeFinder typeFinder)
    {
        
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnumerable<IDHStartup> startups, IWebHostEnvironment webHostEnvironment)
    {
        // 限流
        services.AddRateLimter(options =>
        {
            options.onIntercepted = (context, valve, where) =>
            {
                if (valve.ReturnType == ReturnType.Json_DGResult)
                {
                    return new DGResult(StateCode.Busy, "访问过于频繁，请稍后重试！");
                }
                else if (valve.ReturnType == ReturnType.Json_DResult)
                {
                    return new JsonResult(new DResult { code = 99, msg = "访问过于频繁，请稍后重试！" });
                }
                else
                {
                    return new RateLimterResult { Content = "访问过于频繁，请稍后重试！" };
                }
            };
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
    public int Order => 200;
}
