using DH.Core;
using DH.Core.Configuration;
using DH.Core.Domain.Common;
using DH.Core.Http;
using DH.Core.Infrastructure;
using DH.Core.Security;
using DH.Entity;
using DH.Services.Authentication;
using DH.Services.Authentication.External;
using DH.Services.Common;
using DH.Web.Framework.Captcha;
using DH.Web.Framework.Mvc.ModelBinding;
using DH.Web.Framework.Mvc.ModelBinding.Binders;
using DH.Web.Framework.Mvc.Routing;
using DH.Web.Framework.Themes;
using DH.Web.Framework.Validators;
using DH.Web.Framework.WebOptimizer;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json.Serialization;

using StackExchange.Profiling.Storage;

using System.Net;

using WebMarkupMin.AspNetCore6;
using WebMarkupMin.Core;
using WebMarkupMin.NUglify;

namespace DH.Web.Framework.Infrastructure.Extensions {
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
            WebApplicationBuilder builder, IWebHostEnvironment webHostEnvironment)
        {
            // 让操作系统决定使用什么TLS协议版本
            // 参见https://docs.microsoft.com/dotnet/framework/network-programming/tls
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

            engine.ConfigureServices(services, builder.Configuration, webHostEnvironment);
        }

        /// <summary>
        /// 注册HttpContextAccessor
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// 添加防伪支持所需的服务
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddAntiForgery(this IServiceCollection services)
        {
            // 覆盖cookie名称
            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = $"{DHCookieDefaults.Prefix}{DHCookieDefaults.AntiforgeryCookie}";
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });
        }

        /// <summary>
        /// 添加应用程序会话状态所需的服务
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddHttpSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.Name = $"{DHCookieDefaults.Prefix}{DHCookieDefaults.SessionCookie}";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });
        }

        /// <summary>
        /// 添加主题支持所需的服务
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddThemes(this IServiceCollection services)
        {
            if (!DHSetting.Current.IsInstalled)
                return;

            // 主题支持
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ThemeableViewLocationExpander());
            });
        }

        /// <summary>
        /// 添加分布式缓存所需的服务
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddDistributedCache(this IServiceCollection services)
        {
            var appSettings = Singleton<AppSettings>.Instance;
            var distributedCacheConfig = appSettings.Get<DistributedCacheConfig>();

            if (!distributedCacheConfig.Enabled)
                return;

            switch (distributedCacheConfig.DistributedCacheType)
            {
                case DistributedCacheType.Memory:
                    services.AddDistributedMemoryCache();
                    break;

                case DistributedCacheType.SqlServer:
                    services.AddDistributedSqlServerCache(options =>
                    {
                        options.ConnectionString = distributedCacheConfig.ConnectionString;
                        options.SchemaName = distributedCacheConfig.SchemaName;
                        options.TableName = distributedCacheConfig.TableName;
                    });
                    break;

                case DistributedCacheType.Redis:
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = distributedCacheConfig.ConnectionString;
                    });
                    break;
            }
        }

        /// <summary>
        /// 添加数据保护服务
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddDHDataProtection(this IServiceCollection services)
        {
            var dataProtectionKeysPath = CommonHelper.DefaultFileProvider.MapPath(DHDataProtectionDefaults.DataProtectionKeysPath);
            var dataProtectionKeysFolder = new System.IO.DirectoryInfo(dataProtectionKeysPath);

            // 将数据保护系统配置为将密钥持久化到指定目录
            services.AddDataProtection().PersistKeysToFileSystem(dataProtectionKeysFolder);
        }

        /// <summary>
        /// 添加身份验证服务
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddDHAuthentication(this IServiceCollection services)
        {
            // 设置默认身份验证方案
            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = DHAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = DHAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = DHAuthenticationDefaults.ExternalAuthenticationScheme;
            });

            // 添加主cookie身份验证
            authenticationBuilder.AddCookie(DHAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = $"{DHCookieDefaults.Prefix}{DHCookieDefaults.AuthenticationCookie}";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.LoginPath = DHAuthenticationDefaults.LoginPath;
                options.AccessDeniedPath = DHAuthenticationDefaults.AccessDeniedPath;
            });

            // 添加外部身份验证
            authenticationBuilder.AddCookie(DHAuthenticationDefaults.ExternalAuthenticationScheme, options =>
            {
                options.Cookie.Name = $"{DHCookieDefaults.Prefix}{DHCookieDefaults.ExternalAuthenticationCookie}";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.LoginPath = DHAuthenticationDefaults.LoginPath;
                options.AccessDeniedPath = DHAuthenticationDefaults.AccessDeniedPath;
            });

            // 立即注册和配置外部身份验证插件
            var typeFinder = Singleton<ITypeFinder>.Instance;
            var externalAuthConfigurations = typeFinder.FindClassesOfType<IExternalAuthenticationRegistrar>();
            var externalAuthInstances = externalAuthConfigurations
                .Select(x => (IExternalAuthenticationRegistrar)Activator.CreateInstance(x));

            foreach (var instance in externalAuthInstances)
                instance.Configure(authenticationBuilder);
        }

        /// <summary>
        /// 为应用程序添加和配置MVC
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <returns>用于配置MVC服务的生成器</returns>
        public static IMvcBuilder AddDHMvc(this IServiceCollection services)
        {
            // 添加基本MVC功能
            var mvcBuilder = services.AddControllersWithViews();

            mvcBuilder.AddRazorRuntimeCompilation();

            var appSettings = Singleton<AppSettings>.Instance;
            if (appSettings.Get<CommonConfig>().UseSessionStateTempDataProvider)
            {
                // 使用基于会话的临时数据提供程序
                mvcBuilder.AddSessionStateTempDataProvider();
            }
            else
            {
                // 使用基于cookie的临时数据提供程序
                mvcBuilder.AddCookieTempDataProvider(options =>
                {
                    options.Cookie.Name = $"{DHCookieDefaults.Prefix}{DHCookieDefaults.TempDataCookie}";
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                });
            }

            services.AddRazorPages();

            // MVC现在默认情况下使用驼峰大小写名称序列化JSON，请使用此代码避免它
            mvcBuilder.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // 设置一些选项
            mvcBuilder.AddMvcOptions(options =>
            {
                // 我们会用这个直到 https://github.com/dotnet/aspnetcore/issues/6566已解决 
                options.ModelBinderProviders.Insert(0, new InvariantNumberModelBinderProvider());
                // 添加自定义显示元数据提供者
                options.ModelMetadataDetailsProviders.Add(new DHMetadataProvider());

                //在.NET模型中，不可为null的属性的绑定可能会失败，并显示错误消息"值""无效"。在这里，我们将区域设置名称设置为消息，稍后当非null验证失败时，我们将用实际名称替换它
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => DHValidationDefaults.NotNullValidationLocaleName);
            });

            // 添加流畅验证
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            // 从DH程序集中注册所有可用的验证器
            var assemblies = mvcBuilder.PartManager.ApplicationParts
                .OfType<AssemblyPart>()
                .Where(part => part.Name.StartsWith("DH", StringComparison.InvariantCultureIgnoreCase))
                .Select(part => part.Assembly);
            services.AddValidatorsFromAssemblies(assemblies);

            // 将控制器注册为服务，它将允许覆盖它们
            mvcBuilder.AddControllersAsServices();

            return mvcBuilder;
        }

        /// <summary>
        /// 注册自定义RedirectResultExecutor
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddDHRedirectResultExecutor(this IServiceCollection services)
        {
            // 我们使用自定义重定向执行器作为变通方法，允许在重定向URL中使用非ASCII字符
            services.AddScoped<IActionResultExecutor<RedirectResult>, DHRedirectResultExecutor>();
        }

        /// <summary>
        /// 添加和配置MiniProfiler服务
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddDHMiniProfiler(this IServiceCollection services)
        {
            // 数据库是否已安装
            if (!DHSetting.Current.IsInstalled)
                return;

            var appSettings = Singleton<AppSettings>.Instance;
            if (appSettings.Get<CommonConfig>().MiniProfilerEnabled)
            {
                services.AddMiniProfiler(miniProfilerOptions =>
                {
                    // 使用内存缓存提供程序存储每个结果
                    ((MemoryCacheStorage)miniProfilerOptions.Storage).CacheDuration = TimeSpan.FromMinutes(appSettings.Get<CacheConfig>().DefaultCacheTime);

                    // 确定谁可以访问MiniProfiler结果
                    miniProfilerOptions.ResultsAuthorize = request => UserDetail.IsSuperAdmin();
                });
            }
        }

        /// <summary>
        /// 添加和配置WebMarkupMin服务
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddDHWebMarkupMin(this IServiceCollection services)
        {
            // 检查数据库是否已安装
            if (!DHSetting.Current.IsInstalled)
                return;

            services
                .AddWebMarkupMin(options =>
                {
                    options.AllowMinificationInDevelopmentEnvironment = true;
                    options.AllowCompressionInDevelopmentEnvironment = true;
                    options.DisableMinification = !EngineContext.Current.Resolve<CommonSettings>().EnableHtmlMinification;
                    options.DisableCompression = true;
                    options.DisablePoweredByHttpHeaders = true;
                })
                .AddHtmlMinification(options =>
                {
                    options.MinificationSettings.AttributeQuotesRemovalMode = HtmlAttributeQuotesRemovalMode.KeepQuotes;

                    options.CssMinifierFactory = new NUglifyCssMinifierFactory();
                    options.JsMinifierFactory = new NUglifyJsMinifierFactory();
                })
                .AddXmlMinification(options =>
                {
                    var settings = options.MinificationSettings;
                    settings.RenderEmptyTagsWithSpace = true;
                    settings.CollapseTagsWithoutContent = true;
                });
        }

        /// <summary>
        /// 将WebOptimizer添加到指定的<see cref="IServiceCollection"/>并启用CSS和JavaScript缩小。
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddDHWebOptimizer(this IServiceCollection services)
        {
            var appSettings = Singleton<AppSettings>.Instance;
            var cssBundling = appSettings.Get<WebOptimizerConfig>().EnableCssBundling;
            var jsBundling = appSettings.Get<WebOptimizerConfig>().EnableJavaScriptBundling;

            // 添加缩小和绑定
            var cssSettings = new CssBundlingSettings
            {
                FingerprintUrls = false,
                Minify = cssBundling
            };

            var codeSettings = new CodeBundlingSettings
            {
                Minify = jsBundling,
                AdjustRelativePaths = false // 禁用此功能，因为它会打断结尾有"Url("的函数名
            };

            services.AddWebOptimizer(null, cssSettings, codeSettings);
        }

        /// <summary>
        /// 添加和配置默认HTTP客户端
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        public static void AddDHHttpClients(this IServiceCollection services)
        {
            // 默认客户端
            services.AddHttpClient(DHHttpDefaults.DefaultHttpClient).WithProxy();

            // 客户端请求当前站点
            services.AddHttpClient<StoreHttpClient>();

            // 客户请求官方网站
            services.AddHttpClient<DHHttpClient>().WithProxy();

            // 客户端请求reCAPTCHA服务
            services.AddHttpClient<CaptchaHttpClient>().WithProxy();
        }

    }
}
