using System.Text;

using DH.Core;
using DH.Core.Infrastructure;
using DH.Core.Webs;

using Microsoft.AspNetCore.Http;

using NewLife;

using Pek;
using Pek.Mime;

using ToolGood.Words;

namespace DH.Services.WebMiddleware;

/// <summary>
/// 简繁转换拦截器
/// </summary>
public class TranslateMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next"></param>
    public TranslateMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        if (WebHelper2.IsStaticResource())
        {
            // 或请求管道中调用下一个中间件
            return _next(context);
        }

        var path = context.Request.Path.Value ?? "";
        if (path.StartsWith("/_blazor", StringComparison.OrdinalIgnoreCase) || path.StartsWith("/api", StringComparison.OrdinalIgnoreCase) || path.StartsWith("/file", StringComparison.OrdinalIgnoreCase) || path.StartsWith("/download", StringComparison.OrdinalIgnoreCase) || path.StartsWith("/captcha", StringComparison.OrdinalIgnoreCase) || context.Request.IsRobot() || path.Contains("/health", StringComparison.OrdinalIgnoreCase) || path.Contains("/Site/SiteMap", StringComparison.OrdinalIgnoreCase) || path.StartsWith("/sso", StringComparison.OrdinalIgnoreCase) || path.StartsWith("/Site/Error", StringComparison.OrdinalIgnoreCase))
        {
            return _next(context);
        }

        var workContext = EngineContext.Current.Resolve<IWorkContext>();
        var langs = workContext.WorkingLanguage;
        if (langs.UniqueSeoCode == "cn")
        {
            string lang = context.Request.Query["tlang"];
            lang ??= context.Request.Cookies["tlang"];
            if (lang.IsNullOrEmpty())
            {
                if (context.Request.Location().Contains(new[] { "台湾", "香港", "澳门", "Taiwan", "TW", "HongKong", "HK" }))
                {
                    context.Response.Cookies.Append("tlang", "tw", new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddMinutes(1),
                    });

                    return Traditional(context);
                }

                context.Response.Cookies.Append("tlang", langs.UniqueSeoCode, new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddMinutes(1),
                });

                return _next(context);
            }
            if (lang == "zh-cn" || lang == "cn")
            {
                return _next(context);
            }

            return Traditional(context);
        }

        return _next(context);
    }

    private async Task Traditional(HttpContext context)
    {
        var Accept = context.Request.Headers["Accept"];
        if (!Accept.Any())
        {
            await _next(context).ConfigureAwait(false);
        }
        else
        {
            var accept = context.Request.Headers["Accept"][0];
            if (accept.StartsWith("text") || accept.Contains(MimeTypes.ApplicationJson))
            {
                //设置stream存放ResponseBody
                var responseOriginalBody = context.Response.Body;
                var memStream = new MemoryStream();
                context.Response.Body = memStream;

                // 执行其他中间件
                await _next(context).ConfigureAwait(false);

                // 根据响应的 Content-Type 判断是否为文本内容。二进制文件（Excel、图片等）直接透传，
                // 避免被当作 UTF-8 文本翻译导致文件损坏，并引发 Content-Length 不匹配
                if (!IsTextResponse(context.Response.ContentType))
                {
                    memStream.Position = 0;
                    await memStream.CopyToAsync(responseOriginalBody).ConfigureAwait(false);
                    context.Response.Body = responseOriginalBody;

                    return;
                }

                //处理执行其他中间件后的ResponseBody
                memStream.Position = 0;
                var responseReader = new StreamReader(memStream, Encoding.UTF8);
                var responseBody = await responseReader.ReadToEndAsync().ConfigureAwait(false);
                var bytes = Encoding.UTF8.GetBytes(WordsHelper.ToTraditionalChinese(responseBody));

                // 翻译后字节数会变化，必须清除 Content-Length，交由 Kestrel 重新计算传输长度，否则报 Content-Length mismatch
                context.Response.Headers.ContentLength = null;

                memStream = new MemoryStream(bytes);
                await memStream.CopyToAsync(responseOriginalBody).ConfigureAwait(false);
                context.Response.Body = responseOriginalBody;
            }
            else
            {
                await _next(context).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// 判断响应内容类型是否为可翻译的文本类型
    /// </summary>
    /// <param name="contentType">响应内容类型</param>
    /// <returns>是否为文本类型</returns>
    private static Boolean IsTextResponse(String? contentType)
    {
        if (contentType.IsNullOrEmpty()) return false;

        var mime = contentType.Split(';')[0].Trim().ToLowerInvariant();

        return mime.StartsWith("text/") ||
            mime is "application/json" or "application/xml" or "application/javascript" or "application/xhtml+xml";
    }
}