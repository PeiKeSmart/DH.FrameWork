using Autofac.Extensions.DependencyInjection;

using DH.Core.Configuration;
using DH.Core.Infrastructure;
using DH.Web.Framework.Infrastructure.Extensions;
using DH.Web.Framework.WebMiddleware;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NewLife.Log;

using System.Reflection;

namespace DH.Web.Framework;

/// <summary>基类服务</summary>
public static class CubeService
{
    private static IEngine _engine;

    public readonly static string corsPolicy = "CorsPolicy";

    /// <summary>区域名集合</summary>
    public static String[] AreaNames { get; set; }


    #region 配置基类
    /// <summary>添加基类</summary>
    /// <param name="builder"></param>
    /// <param name="configuration"></param>
    /// <param name="webHostEnvironment"></param>
    /// <returns></returns>
    public static IServiceCollection AddCube(this WebApplicationBuilder builder,
        IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        XTrace.WriteLine("{0} Start 配置系统 {0}", new String('=', 32));
        Assembly.GetExecutingAssembly().WriteVersion();

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Configuration.AddJsonFile(DHConfigurationDefaults.AppSettingsFilePath, true, true);
        if (!string.IsNullOrEmpty(builder.Environment?.EnvironmentName))
        {
            var path = string.Format(DHConfigurationDefaults.AppSettingsEnvironmentFilePath, builder.Environment.EnvironmentName);
            builder.Configuration.AddJsonFile(path, true, true);
        }
        builder.Configuration.AddEnvironmentVariables();


        // 向应用程序添加服务并配置服务提供商
        builder.Services.ConfigureApplicationServices(builder, builder.Environment);


        XTrace.WriteLine("{0} End   配置系统 {0}", new String('=', 32));

        return builder.Services;
    }
    #endregion

    #region 使用基类

    /// <summary>使用基类</summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseCube(this IApplicationBuilder app,
        IConfiguration configuration, IWebHostEnvironment env = null)
    {
        var provider = app.ApplicationServices;

        XTrace.WriteLine("{0} Start 初始化系统 {0}", new String('=', 32));





        app.UseMiddleware<TranslateMiddleware>();  // 简繁转换

        // 配置应用程序HTTP请求管道
        app.ConfigureRequestPipeline();
        app.StartEngine();


        return app;
    }

    #endregion
}
