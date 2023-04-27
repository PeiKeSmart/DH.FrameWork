using DH.Core.Infrastructure;
using DH.VirtualFileSystem;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.AspNetCore;

public class DGStartup : IDHStartup {
    public int Order => 50;

    public void ChangeMenu()
    {

    }

    public void Configure(IApplicationBuilder application, ITypeFinder typeFinder)
    {

    }

    public void ConfigureArea()
    {

    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnumerable<IDHStartup> startups, IWebHostEnvironment webHostEnvironment)
    {
        services.AddSevenZipCompressor().AddResumeFileResult(); // 配置7z和断点续传
    }

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

    public void Update()
    {

    }
}