using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace DH.Api.MUI;

public class ApiUIMiddleware
{
    private const string EmbeddedFileNamespace = "FytApi";

    private readonly ApiUIOptions _options;
    private readonly StaticFileMiddleware _staticFileMiddleware;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ApiUIMiddleware(
        RequestDelegate next,
        IHostingEnvironment hostingEnv,
        ILoggerFactory loggerFactory,
        ApiUIOptions options)
    {
        _options = options ?? new ApiUIOptions();

        _staticFileMiddleware = CreateStaticFileMiddleware(next, hostingEnv, loggerFactory, options);

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true
        };
        _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var httpMethod = httpContext.Request.Method;
        var path = httpContext.Request.Path.Value;
        if (httpMethod == "GET" && Regex.IsMatch(path, $"^/api-config$"))
        {
            await RespondWithConfig(httpContext.Response);
            return;
        }

        await _staticFileMiddleware.Invoke(httpContext);
    }

    private async Task RespondWithConfig(HttpResponse response)
    {
        await response.WriteAsync(JsonSerializer.Serialize(_options.ConfigObject, _jsonSerializerOptions));
    }

    private StaticFileMiddleware CreateStaticFileMiddleware(
        RequestDelegate next,
        IHostingEnvironment hostingEnv,
        ILoggerFactory loggerFactory,
        ApiUIOptions options)
    {
        var staticFileOptions = new StaticFileOptions
        {
            RequestPath = string.IsNullOrEmpty(options.RoutePrefix) ? string.Empty : $"/{options.RoutePrefix}",
            FileProvider = new EmbeddedFileProvider(typeof(ApiUIMiddleware).GetTypeInfo().Assembly, EmbeddedFileNamespace),
        };

        return new StaticFileMiddleware(next, hostingEnv, Options.Create(staticFileOptions), loggerFactory);
    }
}