using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace VueCliMiddleware.PartUI;

public class DHVueMiddleware
{
    private const string EmbeddedFileNamespace = "VueCliMiddleware.PartUI.node_modules.plugincore_admin_frontend.dist";

    private readonly DHVueOptions _options;
    private readonly StaticFileMiddleware _staticFileMiddleware;

    public DHVueMiddleware(
            RequestDelegate next,
            IWebHostEnvironment hostingEnv,
            ILoggerFactory loggerFactory,
            DHVueOptions options)
    {
        _options = options ?? new DHVueOptions();

        _staticFileMiddleware = CreateStaticFileMiddleware(next, hostingEnv, loggerFactory, options);
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var httpMethod = httpContext.Request.Method;
        var path = httpContext.Request.Path.Value;

        // 使用相对重定向来支持代理环境
        if (httpMethod == "GET" && Regex.IsMatch(path, $"^/?{Regex.Escape(_options.RoutePrefix)}/?$", RegexOptions.IgnoreCase))
        {
            // 使用相对重定向来支持代理环境
            var relativeIndexUrl = string.IsNullOrEmpty(path) || path.EndsWith("/")
                ? "index.html"
                : $"{path.Split('/').Last()}/index.html";

            RespondWithRedirect(httpContext.Response, relativeIndexUrl);
            return;
        }

        if (httpMethod == "GET" && Regex.IsMatch(path, $"^/{Regex.Escape(_options.RoutePrefix)}/?index.html$", RegexOptions.IgnoreCase))
        {
            await RespondWithIndexHtml(httpContext.Response);
            return;
        }

        await _staticFileMiddleware.Invoke(httpContext);
    }

    private StaticFileMiddleware CreateStaticFileMiddleware(
            RequestDelegate next,
            IWebHostEnvironment hostingEnv,
            ILoggerFactory loggerFactory,
            DHVueOptions options)
    {
        var dhVueConfig = DHVueConfigFactory.Create();
        IFileProvider fileProvider = null;
        switch (dhVueConfig.FrontendMode?.ToLower())
        {
            default:
            case "localembedded":
                fileProvider = new EmbeddedFileProvider(typeof(DHVueMiddleware).GetTypeInfo().Assembly,
                    EmbeddedFileNamespace);
                break;
            case "localfolder":
                string absoluteRootPath = DHPathProvider.DHVueDir();
                fileProvider = new PhysicalFileProvider(absoluteRootPath);
                break;
            case "remotecdn":
                fileProvider = new DHVueUIRemoteFileProvider(dhVueConfig.RemoteFrontend);
                break;
        }

        var staticFileOptions = new StaticFileOptions
        {
            RequestPath = string.IsNullOrEmpty(options.RoutePrefix) ? string.Empty : $"/{options.RoutePrefix}",
            FileProvider = fileProvider,
        };

        return new StaticFileMiddleware(next, hostingEnv, Options.Create(staticFileOptions), loggerFactory);
    }

    private void RespondWithRedirect(HttpResponse response, string location)
    {
        response.StatusCode = 301;
        response.Headers["Location"] = location;
    }

    private async Task RespondWithIndexHtml(HttpResponse response)
    {
        response.StatusCode = 200;
        response.ContentType = "text/html;charset=utf-8";

        using (var stream = _options.IndexStream())
        {
            // 在写入响应之前注入参数
            var htmlBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());
            foreach (var entry in GetIndexArguments())
            {
                htmlBuilder.Replace(entry.Key, entry.Value);
            }

            await response.WriteAsync(htmlBuilder.ToString(), Encoding.UTF8);
        }
    }

    private IDictionary<string, string> GetIndexArguments()
    {
        return new Dictionary<string, string>()
        {
            //{ "%(DocumentTitle)", _options.DocumentTitle },
            //{ "%(HeadContent)", _options.HeadContent },
            //{ "%(ConfigObject)", JsonSerializer.Serialize(_options.ConfigObject, _jsonSerializerOptions) },
            //{ "%(OAuthConfigObject)", JsonSerializer.Serialize(_options.OAuthConfigObject, _jsonSerializerOptions) },
            //{ "%(Interceptors)", JsonSerializer.Serialize(_options.Interceptors) },
        };
    }

}
