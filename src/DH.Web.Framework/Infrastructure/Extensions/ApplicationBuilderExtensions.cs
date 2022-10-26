using DH.Core;
using DH.Core.Configuration;
using DH.Core.Domain.Common;
using DH.Core.Http;
using DH.Core.Infrastructure;
using DH.Entity;
using DH.Services.Authentication;
using DH.Services.Common;
using DH.Services.Installation;
using DH.Services.Media.RoxyFileman;
using DH.Services.Plugins;
using DH.Services.ScheduleTasks;
using DH.Services.Seo;
using DH.Web.Framework.Globalization;
using DH.Web.Framework.Mvc.Routing;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

using NewLife;
using NewLife.Log;

using System.Globalization;
using System.Net;
using System.Runtime.ExceptionServices;

using WebMarkupMin.AspNetCore6;

using WebOptimizer;

using XCode.Membership;

namespace DH.Web.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// 表示IApplicationBuilder的扩展
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 配置应用程序HTTP请求管道
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        public static void StartEngine(this IApplicationBuilder application)
        {
            var engine = EngineContext.Current;

            // 只有在安装数据库时才执行进一步的操作
            if (DHSetting.Current.IsInstalled)
            {
                // 应用程序启动
                XTrace.WriteLine("应用程序已启动");
                LogProvider.Provider?.WriteLog("启动", "成功", true, $"应用程序已启动", 0);

                // 安装和更新插件
                var pluginService = engine.Resolve<IPluginService>();
                pluginService.InstallPluginsAsync().Wait();
                pluginService.UpdatePluginsAsync().Wait();

                var taskScheduler = engine.Resolve<ITaskScheduler>();
                taskScheduler.InitializeAsync().Wait();
                taskScheduler.StartScheduler();
            }
        }

        /// <summary>
        /// 添加异常处理
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseDHExceptionHandler(this IApplicationBuilder application)
        {
            var appSettings = EngineContext.Current.Resolve<AppSettings>();
            var webHostEnvironment = EngineContext.Current.Resolve<IWebHostEnvironment>();
            var useDetailedExceptionPage = appSettings.Get<CommonConfig>().DisplayFullErrorStack || webHostEnvironment.IsDevelopment();
            if (useDetailedExceptionPage)
            {
                // 为开发和测试目的获取详细的异常
                application.UseDeveloperExceptionPage();
            }
            else
            {
                // 或使用特殊异常处理程序
                application.UseExceptionHandler("/Error/Error");
            }

            // 日志错误
            application.UseExceptionHandler(handler =>
            {
                handler.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (exception == null)
                        return;

                    try
                    {
                        // 检查数据库是否已安装
                        if (DHSetting.Current.IsInstalled)
                        {
                            var webHelper = EngineContext.Current.Resolve<IWebHelper>();

                            // 获取当前客户
                            var currentCustomer = EngineContext.Current.Resolve<IWorkContext>().GetCurrentCustomer();

                            // 错误日志
                            XTrace.WriteException(exception);
                            LogProvider.Provider?.WriteLog("系统", "错误", false, exception.Message + " " + Environment.NewLine + exception.GetMessage(), currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
                        }
                    }
                    finally
                    {
                        // 重新引发异常以显示错误页
                        ExceptionDispatchInfo.Throw(exception);
                    }

                    await Task.CompletedTask;
                });
            });
        }

        /// <summary>
        /// 添加一个特殊处理程序，用于检查没有正文的404状态代码的响应
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UsePageNotFound(this IApplicationBuilder application)
        {
            application.UseStatusCodePages(async context =>
            {
                // 404(找不到)
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                    if (!webHelper.IsStaticResource())
                    {
                        // 获取原始路径和查询
                        var originalPath = context.HttpContext.Request.Path;
                        var originalQueryString = context.HttpContext.Request.QueryString;

                        if (DHSetting.Current.IsInstalled)
                        {
                            var commonSettings = EngineContext.Current.Resolve<CommonSettings>();

                            if (commonSettings.Log404Errors)
                            {
                                var workContext = EngineContext.Current.Resolve<IWorkContext>();

                                // 获取当前客户
                                var currentCustomer = workContext.GetCurrentCustomer();

                                var message = $"Error 404. The requested page ({originalPath}) was not found";
                                XTrace.Log.Error(message);
                                LogProvider.Provider?.WriteLog("系统", "错误", false, message, currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
                            }
                        }

                        try
                        {
                            // 获取新路径
                            var pageNotFoundPath = "/page-not-found";
                            // 使用新路径重新执行请求
                            context.HttpContext.Response.Redirect(context.HttpContext.Request.PathBase + pageNotFoundPath);
                        }
                        finally
                        {
                            // 返回请求的原始路径
                            context.HttpContext.Request.QueryString = originalQueryString;
                            context.HttpContext.Request.Path = originalPath;
                        }
                    }
                }

                await Task.CompletedTask;
            });
        }

        /// <summary>
        /// 添加一个特殊处理程序，用于检查具有400状态代码的响应（错误请求）
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseBadRequestResult(this IApplicationBuilder application)
        {
            application.UseStatusCodePages(async context =>
            {
                // 400（错误请求）
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
                {
                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                    var workContext = EngineContext.Current.Resolve<IWorkContext>();

                    // 获取当前客户
                    var currentCustomer = workContext.GetCurrentCustomer();

                    var message = $"Error 400. Bad request";
                    XTrace.Log.Error(message);
                    LogProvider.Provider?.WriteLog("系统", "错误", false, message, currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
                }

                await Task.CompletedTask;
            });
        }

        /// <summary>
        /// 配置中间件以动态压缩HTTP响应
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseDHResponseCompression(this IApplicationBuilder application)
        {
            if (!DHSetting.Current.IsInstalled)
                return;

            // 是否使用压缩（默认情况下为gzip）
            if (EngineContext.Current.Resolve<CommonSettings>().UseResponseCompression)
                application.UseResponseCompression();
        }

        /// <summary>
        /// 将WebOptimizer添加到<see cref="IApplicationBuilder"/>请求执行管道
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseDHWebOptimizer(this IApplicationBuilder application)
        {
            var fileProvider = EngineContext.Current.Resolve<IDHFileProvider>();
            var webHostEnvironment = EngineContext.Current.Resolve<IWebHostEnvironment>();

            application.UseWebOptimizer(webHostEnvironment, new[]
            {
                new FileProviderOptions
                {
                    RequestPath =  new PathString("/Plugins"),
                    FileProvider = new PhysicalFileProvider(fileProvider.MapPath(@"Plugins"))
                },
                new FileProviderOptions
                {
                    RequestPath =  new PathString("/Themes"),
                    FileProvider = new PhysicalFileProvider(fileProvider.MapPath(@"Themes"))
                }
            });
        }

        /// <summary>
        /// 配置静态文件服务
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseDHStaticFiles(this IApplicationBuilder application)
        {
            var fileProvider = EngineContext.Current.Resolve<IDHFileProvider>();
            var appSettings = EngineContext.Current.Resolve<AppSettings>();

            void staticFileResponse(StaticFileResponseContext context)
            {
                if (!string.IsNullOrEmpty(appSettings.Get<CommonConfig>().StaticFilesCacheControl))
                    context.Context.Response.Headers.Append(HeaderNames.CacheControl, appSettings.Get<CommonConfig>().StaticFilesCacheControl);
            }

            // 如果站点地图，则添加处理
            application.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(fileProvider.GetAbsolutePath(DHSeoDefaults.SitemapXmlDirectory)),
                RequestPath = new PathString($"/{DHSeoDefaults.SitemapXmlDirectory}"),
                OnPrepareResponse = context =>
                {
                    if (!DHSetting.Current.IsInstalled ||
                        !EngineContext.Current.Resolve<SitemapXmlSettings>().SitemapXmlEnabled)
                    {
                        context.Context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Context.Response.ContentLength = 0;
                        context.Context.Response.Body = Stream.Null;
                    }
                }
            });

            // 通用静态文件
            application.UseStaticFiles(new StaticFileOptions { OnPrepareResponse = staticFileResponse });

            // 主题静态文件
            application.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(fileProvider.MapPath(@"Themes")),
                RequestPath = new PathString("/Themes"),
                OnPrepareResponse = staticFileResponse
            });

            // 插件静态文件
            var staticFileOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(fileProvider.MapPath(@"Plugins")),
                RequestPath = new PathString("/Plugins"),
                OnPrepareResponse = staticFileResponse
            };

            // 排除黑名单中的文件
            if (!string.IsNullOrEmpty(appSettings.Get<CommonConfig>().PluginStaticFileExtensionsBlacklist))
            {
                var fileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();

                foreach (var ext in appSettings.Get<CommonConfig>().PluginStaticFileExtensionsBlacklist
                    .Split(';', ',')
                    .Select(e => e.Trim().ToLowerInvariant())
                    .Select(e => $"{(e.StartsWith(".") ? string.Empty : ".")}{e}")
                    .Where(fileExtensionContentTypeProvider.Mappings.ContainsKey))
                {
                    fileExtensionContentTypeProvider.Mappings.Remove(ext);
                }

                staticFileOptions.ContentTypeProvider = fileExtensionContentTypeProvider;
            }

            application.UseStaticFiles(staticFileOptions);

            // 添加对备份的支持
            var provider = new FileExtensionContentTypeProvider
            {
                Mappings = { [".bak"] = MimeTypes.ApplicationOctetStream }
            };

            application.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(fileProvider.GetAbsolutePath(DHCommonDefaults.DbBackupsPath)),
                RequestPath = new PathString("/db_backups"),
                ContentTypeProvider = provider,
                OnPrepareResponse = context =>
                {
                    if (!DHSetting.Current.IsInstalled ||
                        !UserDetail.IsSuperAdmin())
                    {
                        context.Context.Response.StatusCode = StatusCodes.Status404NotFound;
                        context.Context.Response.ContentLength = 0;
                        context.Context.Response.Body = Stream.Null;
                    }
                }
            });

            // 添加对webmanifest文件的支持
            provider.Mappings[".webmanifest"] = MimeTypes.ApplicationManifestJson;

            application.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(fileProvider.GetAbsolutePath("icons")),
                RequestPath = "/icons",
                ContentTypeProvider = provider
            });

            if (DHSetting.Current.IsInstalled)
            {
                application.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new RoxyFilemanProvider(fileProvider.GetAbsolutePath(DHRoxyFilemanDefaults.DefaultRootDirectory.TrimStart('/').Split('/'))),
                    RequestPath = new PathString(DHRoxyFilemanDefaults.DefaultRootDirectory),
                    OnPrepareResponse = staticFileResponse
                });
            }

            if (appSettings.Get<CommonConfig>().ServeUnknownFileTypes)
            {
                application.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(fileProvider.GetAbsolutePath(".well-known")),
                    RequestPath = new PathString("/.well-known"),
                    ServeUnknownFileTypes = true,
                });
            }
        }

        /// <summary>
        /// 配置中间件检查请求的页面是否为保持活动页面
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseKeepAlive(this IApplicationBuilder application)
        {
            application.UseMiddleware<KeepAliveMiddleware>();
        }

        /// <summary>
        /// 配置中间件检查数据库是否已安装
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseInstallUrl(this IApplicationBuilder application)
        {
            application.UseMiddleware<InstallUrlMiddleware>();
        }

        /// <summary>
        /// 添加身份验证中间件，它启用身份验证功能。
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseDHAuthentication(this IApplicationBuilder application)
        {
            // 检查数据库是否已安装
            if (!DHSetting.Current.IsInstalled)
                return;

            application.UseMiddleware<AuthenticationMiddleware>();
        }

        /// <summary>
        /// 配置请求本地化功能
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseDHRequestLocalization(this IApplicationBuilder application)
        {
            application.UseRequestLocalization(options =>
            {
                if (!DHSetting.Current.IsInstalled)
                    return;

                // 准备支持的区域性
                var cultures = Language.GetAllLanguages()
                    .Select(language => new CultureInfo(language.LanguageCulture)).ToList();
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
                options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault());
                options.ApplyCurrentCultureToResponseHeaders = true;

                // 配置区域性提供程序
                options.AddInitialRequestCultureProvider(new DHSeoUrlCultureProvider());
                var cookieRequestCultureProvider = options.RequestCultureProviders.OfType<CookieRequestCultureProvider>().FirstOrDefault();
                if (cookieRequestCultureProvider is not null)
                    cookieRequestCultureProvider.CookieName = $"{DHCookieDefaults.Prefix}{DHCookieDefaults.CultureCookie}";
            });
        }

        /// <summary>
        /// 配置端点路由
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseDHEndpoints(this IApplicationBuilder application)
        {
            // 执行路由中间件选择的端点
            application.UseEndpoints(endpoints =>
            {
                // 注册所有路由
                EngineContext.Current.Resolve<IRoutePublisher>().RegisterRoutes(endpoints);
            });
        }

        /// <summary>
        /// 配置将转发的标头应用于当前请求的匹配字段。
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseDHProxy(this IApplicationBuilder application)
        {
            var appSettings = EngineContext.Current.Resolve<AppSettings>();

            if (appSettings.Get<HostingConfig>().UseProxy)
            {
                var options = new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.All,
                    // IIS已充当反向代理，并将向所有请求添加X-Forwarded标头，
                    // 所以我们需要增加这个限制，否则，传递的转发头将被忽略。
                    ForwardLimit = 2
                };

                if (!string.IsNullOrEmpty(appSettings.Get<HostingConfig>().ForwardedForHeaderName))
                    options.ForwardedForHeaderName = appSettings.Get<HostingConfig>().ForwardedForHeaderName;

                if (!string.IsNullOrEmpty(appSettings.Get<HostingConfig>().ForwardedProtoHeaderName))
                    options.ForwardedProtoHeaderName = appSettings.Get<HostingConfig>().ForwardedProtoHeaderName;

                if (!string.IsNullOrEmpty(appSettings.Get<HostingConfig>().KnownProxies))
                {
                    foreach (var strIp in appSettings.Get<HostingConfig>().KnownProxies.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                    {
                        if (IPAddress.TryParse(strIp, out var ip))
                            options.KnownProxies.Add(ip);
                    }

                    if (options.KnownProxies.Count > 1)
                        options.ForwardLimit = null; // 禁用限制，因为已配置KnownProxies
                }

                // 配置转发
                application.UseForwardedHeaders(options);
            }
        }

        /// <summary>
        /// 配置WebMarkupMin
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        public static void UseDHWebMarkupMin(this IApplicationBuilder application)
        {
            // 检查数据库是否已安装
            if (!DHSetting.Current.IsInstalled)
                return;

            application.UseWebMarkupMin();
        }
    }
}
