using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NewLife.Log;

using System.Text;

namespace LettuceEncrypt.Internal;

internal class HttpChallengeResponseMiddleware : IMiddleware
{
    private readonly IHttpChallengeResponseStore _responseStore;
    private readonly ILogger<HttpChallengeResponseMiddleware> _logger;
    private readonly IOptions<LettuceEncryptOptions> _options;

    public HttpChallengeResponseMiddleware(
        IHttpChallengeResponseStore responseStore,
        ILogger<HttpChallengeResponseMiddleware> logger, IOptions<LettuceEncryptOptions> options)
    {
        _responseStore = responseStore;
        _logger = logger;
        _options = options;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        XTrace.WriteLine($"进来/.well-known/acme-challenge路径的中间件了么？");

        // 假设该中间件已经映射
        var token = context.Request.Path.ToString();
        if (token.StartsWith('/'))
        {
            token = token[1..];
        }

        if (!_responseStore.TryGetResponse(token, out var value))
        {
            var file = $".well-known/acme-challenge/{token}".AsFile();
            if (file.Exists)
            {
                value = Encoding.UTF8.GetString(file.ReadBytes());
            }
            else
            {
                file = $"../.well-known/acme-challenge/{token}".AsFile();
                if (file.Exists)
                {
                    value = Encoding.UTF8.GetString(file.ReadBytes());
                }
                else
                {
                    XTrace.WriteLine($"未确认{token}的质询请求:{value}");
                    await next(context);
                    return;
                }
            }
        }

        XTrace.WriteLine($"已确认{token}的质询请求:{value}");

        context.Response.ContentLength = value?.Length ?? 0;
        context.Response.ContentType = "application/octet-stream";
        await context.Response.WriteAsync(value!, context.RequestAborted);
    }
}
