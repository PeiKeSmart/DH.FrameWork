namespace DH.SignalR;

public class SignalRMiddleware
{
    private readonly RequestDelegate _next;

    public SignalRMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var request = httpContext.Request;

        // 网络套接字无法传递标头，因此我们必须从查询参数中获取访问令牌，
        // 在身份验证中间件运行之前将其添加到标头
        if (request.Path.StartsWithSegments("/notify-hub", StringComparison.OrdinalIgnoreCase) &&
            request.Query.TryGetValue("access_token", out var accessToken))
        {
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
        }

        await _next(httpContext);
    }
}