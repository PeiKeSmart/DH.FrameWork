using DH.Core.Infrastructure;
using DH.VirtualFileSystem.Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NewLife.Log;

namespace DH.VirtualFileSystem;

public partial class DHStartup : IDHStartup
{
    /// <summary>
    /// 获取此启动配置实现的顺序
    /// </summary>
    public int Order => 51;

    /// <summary>
    /// 配置添加的中间件的使用
    /// </summary>
    /// <param name="application">用于配置应用程序的请求管道的生成器</param>
    public void Configure(IApplicationBuilder application)
    {
        application.UseVirtualFiles();
    }

    /// <summary>
    /// 添加并配置任何中间件
    /// </summary>
    /// <param name="services">服务描述符集合</param>
    /// <param name="configuration">应用程序的配置</param>
    /// <param name="startups">查找到的IDHStartup集合</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnumerable<IDHStartup> startups, IWebHostEnvironment webHostEnvironment)
    {
        // 注册虚拟文件系统
        services.AddVFS();

        // 注入当前嵌入到虚拟文件系统
        services.Configure<DHVirtualFileSystemOptions>(options =>
        {
            foreach (var instance in startups)
                instance.ConfigureVirtualFileSystem(options);
        });
    }

    /// <summary>
    /// 配置虚拟文件系统
    /// </summary>
    /// <param name="options">虚拟文件配置</param>
    public void ConfigureVirtualFileSystem(DHVirtualFileSystemOptions options)
    {

    }
}
