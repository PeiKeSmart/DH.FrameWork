using DH.Core;
using DH.Core.Caching;
using DH.Core.Configuration;
using DH.Core.Events;
using DH.Core.Infrastructure;
using DH.Services.Common;
using DH.Services.Configuration;
using DH.Services.Customers;
using DH.Services.Events;
using DH.Services.Helpers;
using DH.Services.Plugins;
using DH.Services.Plugins.Marketplace;
using DH.Services.ScheduleTasks;
using DH.Services.Themes;
using DH.Web.Framework.Mvc.Routing;
using DH.Web.Framework.Themes;
using DH.Web.Framework.UI;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using NewLife.Caching;

namespace DH.Web.Framework.Infrastructure
{
    /// <summary>
    /// 表示应用程序启动时注册服务
    /// </summary>
    public partial class DHStartup : IDHStartup
    {
        /// <summary>
        /// 获取此启动配置实现的顺序
        /// </summary>
        public int Order => 2000;

        // <summary>
        /// 文件提供程序
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public void Configure(IApplicationBuilder application)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 文件提供程序
            services.AddScoped<IDHFileProvider, DHFileProvider>();

            // web助手
            services.AddScoped<IWebHelper, WebHelper>();

            //UserAgent帮助
            services.AddScoped<IUserAgentHelper, UserAgentHelper>();

            // 插件
            services.AddScoped<IPluginService, PluginService>();
            services.AddScoped<OfficialFeedManager>();

            // 静态缓存管理器
            var appSettings = Singleton<AppSettings>.Instance;
            var distributedCacheConfig = appSettings.Get<DistributedCacheConfig>();
            if (distributedCacheConfig.Enabled)
            {
                switch (distributedCacheConfig.DistributedCacheType)
                {
                    case DistributedCacheType.Memory:
                        services.AddScoped<ILocker, MemoryDistributedCacheManager>();
                        services.AddScoped<IStaticCacheManager, MemoryDistributedCacheManager>();
                        break;
                    case DistributedCacheType.SqlServer:
                        services.AddScoped<ILocker, MsSqlServerCacheManager>();
                        services.AddScoped<IStaticCacheManager, MsSqlServerCacheManager>();
                        break;
                    case DistributedCacheType.Redis:
                        services.AddScoped<ILocker, RedisCacheManager>();
                        services.AddScoped<IStaticCacheManager, RedisCacheManager>();
                        break;
                }
            }
            else
            {
                services.AddSingleton<ILocker, MemoryCacheManager>();
                services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
            }

            // 工作上下文
            services.AddScoped<IWorkContext, WebWorkContext>();

            // 站点上下文
            services.AddScoped<IStoreContext, WebStoreContext>();



            // 缓存
            services.TryAddSingleton(Cache.Default);

            services.AddScoped<IGenericAttributeService, GenericAttributeService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IDHHtmlHelper, DHHtmlHelper>();
            services.AddScoped<IThemeProvider, ThemeProvider>();
            services.AddScoped<IThemeContext, ThemeContext>();
            services.AddSingleton<IRoutePublisher, RoutePublisher>();
            services.AddSingleton<IEventPublisher, EventPublisher>();
            services.AddScoped<ISettingService, SettingService>();


            //plugin managers
            services.AddScoped(typeof(IPluginManager<>), typeof(PluginManager<>));


            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // 注册所有设置
            var typeFinder = Singleton<ITypeFinder>.Instance;

            var settings = typeFinder.FindClassesOfType(typeof(ISettings), false).ToList();
            foreach (var setting in settings)
            {
                services.AddScoped(setting, serviceProvider =>
                {
                    var storeId = DHSetting.Current.IsInstalled
                        ? serviceProvider.GetRequiredService<IStoreContext>().GetCurrentStore()?.Id ?? 0
                        : 0;

                    return serviceProvider.GetRequiredService<ISettingService>().LoadSetting(setting, storeId);
                });
            }


            // 分段路由处理
            if (DHSetting.Current.IsInstalled)
                services.AddScoped<SlugRouteTransformer>();

            // 计划任务
            services.AddSingleton<ITaskScheduler, DH.Services.ScheduleTasks.TaskScheduler>();
            services.AddTransient<IScheduleTaskRunner, ScheduleTaskRunner>();

            // 事件消费者
            var consumers = typeFinder.FindClassesOfType(typeof(IConsumer<>)).ToList();
            foreach (var consumer in consumers)
                foreach (var findInterface in consumer.FindInterfaces((type, criteria) =>
                {
                    var isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
                    return isMatch;
                }, typeof(IConsumer<>)))
                    services.AddScoped(findInterface, consumer);

        }
    }
}
