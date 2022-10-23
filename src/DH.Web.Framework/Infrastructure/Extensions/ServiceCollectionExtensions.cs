using DH.Core;
using DH.Core.Configuration;
using DH.Core.Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Net;

namespace DH.Web.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// 表示IServiceCollection的扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 向应用程序添加服务并配置服务提供商
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <param name="builder">web应用程序和服务的生成器</param>
        public static void ConfigureApplicationServices(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            //让操作系统决定使用什么TLS协议版本
            //参见https://docs.microsoft.com/dotnet/framework/network-programming/tls
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

            // 创建默认文件提供程序
            CommonHelper.DefaultFileProvider = new DHFileProvider(builder.Environment);

            // 将访问器添加到HttpContext
            services.AddHttpContextAccessor();

            // 初始化插件
            var mvcCoreBuilder = services.AddMvcCore();
            var pluginConfig = new PluginConfig();
            builder.Configuration.GetSection(nameof(PluginConfig)).Bind(pluginConfig, options => options.BindNonPublicProperties = true);
            mvcCoreBuilder.PartManager.InitializePlugins(pluginConfig);

            // 注册类型查找器
            var typeFinder = new WebAppTypeFinder();
            Singleton<ITypeFinder>.Instance = typeFinder;
            services.AddSingleton<ITypeFinder>(typeFinder);

            // 添加配置参数
            var configurations = typeFinder
                .FindClassesOfType<IConfig>()
                .Select(configType => (IConfig)Activator.CreateInstance(configType))
                .ToList();
            foreach (var config in configurations)
            {
                builder.Configuration.GetSection(config.Name).Bind(config, options => options.BindNonPublicProperties = true);
            }
            var appSettings = AppSettingsHelper.SaveAppSettings(configurations, CommonHelper.DefaultFileProvider, false);
            services.AddSingleton(appSettings);

            // 创建引擎并配置服务提供商
            var engine = EngineContext.Create();

            engine.ConfigureServices(services, builder.Configuration);
        }

        /// <summary>
        /// 注册HttpContextAccessor
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

    }
}
