using DH.AspNetCore.Files;
using DH.AspNetCore.ResumeFileResult;
using DH.AspNetCore.ResumeFileResult.Executor;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DH.AspNetCore;

/// <summary>
/// 依赖注入ServiceCollection容器扩展方法
/// </summary>
public static class ServiceCollectionExtensions {
    /// <summary>
    /// 注入断点续传服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddResumeFileResult(this IServiceCollection services)
    {
        services.TryAddSingleton<IActionResultExecutor<ResumePhysicalFileResult>, ResumePhysicalFileResultExecutor>();
        services.TryAddSingleton<IActionResultExecutor<ResumeVirtualFileResult>, ResumeVirtualFileResultExecutor>();
        services.TryAddSingleton<IActionResultExecutor<ResumeFileStreamResult>, ResumeFileStreamResultExecutor>();
        services.TryAddSingleton<IActionResultExecutor<ResumeFileContentResult>, ResumeFileContentResultExecutor>();
        return services;
    }

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

    /// <summary>
    /// 注入HttpContext静态对象，方便在任意地方获取HttpContext，services.AddHttpContextAccessor();
    /// </summary>
    /// <param name="services"></param>
    public static void AddStaticHttpContext(this IServiceCollection services)
    {
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
}