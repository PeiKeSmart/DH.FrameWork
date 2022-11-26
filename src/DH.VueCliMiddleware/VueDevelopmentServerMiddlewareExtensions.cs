using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices;

using System;

namespace VueCliMiddleware
{
    /// <summary>
    /// 用于启用Vue开发服务器中间件支持的扩展方法。
    /// </summary>
    public static class VueCliMiddlewareExtensions
    {
        /// <summary>
        /// 通过将请求传递给vue-cli服务器的实例来处理请求。
        /// 这意味着您可以始终提供最新的CLI构建资源，而无需
        /// 手动运行vue-cli服务器。
        ///
        /// 此功能只能在开发中使用。对于生产部署确保不要启用vue-cli服务器。
        /// </summary>
        /// <param name="spaBuilder"><see cref="ISpaBuilder"/>对像.</param>
        /// <param name="npmScript">包中脚本的名称。json文件，启动vue-cli服务器。</param>
        /// <param name="port">指定vue-cli服务器端口号。如果&lt；80，使用随机端口。</param>
        /// <param name="https">指定vue-cli服务器模式</param>
        /// <param name="runner">指定runner、Npm和Yarn是有效选项。纱线支撑高度实验性。</param>
        /// <param name="regex">指定要在日志中搜索的自定义regex字符串，指示代理服务器正在运行。VueCli:“正在运行”，QuasarCli:“编译成功”</param>
        /// <param name="forceKill"></param>
        /// <param name="wsl"></param>
        public static void UseVueCli(
            this ISpaBuilder spaBuilder,
            string npmScript = "serve",
            int port = 8080,
            bool https = false,
            ScriptRunnerType runner = ScriptRunnerType.Npm,
            string regex = VueCliMiddleware.DefaultRegex,
            bool forceKill = false,
            bool wsl = false)
        {
            if (spaBuilder == null)
            {
                throw new ArgumentNullException(nameof(spaBuilder));
            }

            var spaOptions = spaBuilder.Options;

            if (string.IsNullOrEmpty(spaOptions.SourcePath))
            {
                throw new InvalidOperationException($"To use {nameof(UseVueCli)}, you must supply a non-empty value for the {nameof(SpaOptions.SourcePath)} property of {nameof(SpaOptions)} when calling {nameof(SpaApplicationBuilderExtensions.UseSpa)}.");
            }

            VueCliMiddleware.Attach(spaBuilder, npmScript, port, https: https, runner: runner, regex: regex, forceKill: forceKill, wsl: wsl);
        }


        public static IEndpointConventionBuilder MapToVueCliProxy(
            this IEndpointRouteBuilder endpoints,
            string pattern,
            SpaOptions options,
            string npmScript = "serve",
            int port = 8080,
            bool https = false,
            ScriptRunnerType runner = ScriptRunnerType.Npm,
            string regex = VueCliMiddleware.DefaultRegex,
            bool forceKill = false,
            bool wsl = false)
        {
            if (pattern == null) { throw new ArgumentNullException(nameof(pattern)); }
            return endpoints.MapFallback(pattern, CreateProxyRequestDelegate(endpoints, options, npmScript, port, https, runner, regex, forceKill, wsl));
        }

        public static IEndpointConventionBuilder MapToVueCliProxy(
            this IEndpointRouteBuilder endpoints,
            string pattern,
            string sourcePath,
            string npmScript = "serve",
            int port = 8080,
            bool https = false,
            ScriptRunnerType runner = ScriptRunnerType.Npm,
            string regex = VueCliMiddleware.DefaultRegex,
            bool forceKill = false,
            bool wsl = false)
        {
            if (pattern == null) { throw new ArgumentNullException(nameof(pattern)); }
            if (sourcePath == null) { throw new ArgumentNullException(nameof(sourcePath)); }
            return endpoints.MapFallback(pattern, CreateProxyRequestDelegate(endpoints, new SpaOptions { SourcePath = sourcePath }, npmScript, port, https, runner, regex, forceKill, wsl));
        }

        public static IEndpointConventionBuilder MapToVueCliProxy(
            this IEndpointRouteBuilder endpoints,
            SpaOptions options,
            string npmScript = "serve",
            int port = 8080,
            bool https = false,
            ScriptRunnerType runner = ScriptRunnerType.Npm,
            string regex = VueCliMiddleware.DefaultRegex,
            bool forceKill = false,
            bool wsl = false)
        {
            return endpoints.MapFallback("{*path}", CreateProxyRequestDelegate(endpoints, options, npmScript, port, https, runner, regex, forceKill, wsl));
        }

        public static IEndpointConventionBuilder MapToVueCliProxy(
            this IEndpointRouteBuilder endpoints,
            string sourcePath,
            string npmScript = "serve",
            int port = 8080,
            bool https = false,
            ScriptRunnerType runner = ScriptRunnerType.Npm,
            string regex = VueCliMiddleware.DefaultRegex,
            bool forceKill = false,
            bool wsl = false)
        {
            if (sourcePath == null) { throw new ArgumentNullException(nameof(sourcePath)); }
            return endpoints.MapFallback("{*path}", CreateProxyRequestDelegate(endpoints, new SpaOptions { SourcePath = sourcePath }, npmScript, port, https, runner, regex, forceKill, wsl));
        }



        private static RequestDelegate CreateProxyRequestDelegate(
            IEndpointRouteBuilder endpoints,
            SpaOptions options,
            string npmScript = "serve",
            int port = 8080,
            bool https = false,
            ScriptRunnerType runner = ScriptRunnerType.Npm,
            string regex = VueCliMiddleware.DefaultRegex,
            bool forceKill = false,
            bool wsl = false)
        {
            // based on CreateRequestDelegate() https://github.com/aspnet/AspNetCore/blob/master/src/Middleware/StaticFiles/src/StaticFilesEndpointRouteBuilderExtensions.cs#L194

            if (endpoints == null) { throw new ArgumentNullException(nameof(endpoints)); }
            if (options == null) { throw new ArgumentNullException(nameof(options)); }
            //if (npmScript == null) { throw new ArgumentNullException(nameof(npmScript)); }

            var app = endpoints.CreateApplicationBuilder();
            app.Use(next => context =>
            {
                // Set endpoint to null so the SPA middleware will handle the request.
                context.SetEndpoint(null);
                return next(context);
            });

            app.UseSpa(opt =>
            {
                if (options != null)
                {
                    opt.Options.DefaultPage = options.DefaultPage;
                    opt.Options.DefaultPageStaticFileOptions = options.DefaultPageStaticFileOptions;
                    opt.Options.SourcePath = options.SourcePath;
                    opt.Options.StartupTimeout = options.StartupTimeout;
                }

                if (!string.IsNullOrWhiteSpace(npmScript))
                {
                    opt.UseVueCli(npmScript, port, https, runner, regex, forceKill, wsl);
                }
            });

            return app.Build();
        }
    }
}