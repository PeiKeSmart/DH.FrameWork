using Microsoft.Extensions.DependencyInjection;

namespace DH.VirtualFileSystem;

/// <summary>
/// 扩展服务
/// </summary>
public static partial class Extensions
{
    /// <summary>
    /// 注册Jwt服务
    /// </summary>
    /// <param name="services">服务集合</param>
    public static void AddVFS(this IServiceCollection services)
    {
        services.Configure<DHAspNetCoreContentOptions>(x => new DHAspNetCoreContentOptions());

        services.AddSingleton<IDynamicFileProvider, DynamicFileProvider>();

        services.AddSingleton<IVirtualFileProvider, VirtualFileProvider>();
        services.AddSingleton<IWebContentFileProvider, WebContentFileProvider>();
    }
}
