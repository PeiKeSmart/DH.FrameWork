using DH.AspNetCore.Files;

using Microsoft.Extensions.DependencyInjection;

namespace DH.AspNetCore;

/// <summary>
/// 依赖注入ServiceCollection容器扩展方法
/// </summary>
public static class ServiceCollectionExtensions {
    /// <summary>
    /// 注入7z压缩
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSevenZipCompressor(this IServiceCollection services)
    {
        services.AddHttpClient<ISevenZipCompressor, SevenZipCompressor>();
        return services;
    }
}