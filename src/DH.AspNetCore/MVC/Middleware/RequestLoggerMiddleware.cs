using System.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using NewLife;
using NewLife.Collections;
using NewLife.Log;
using NewLife.Serialization;

using Pek.Models;

namespace DH.AspNetCore.Middleware;

/// <summary>
///  请求日志中间件
/// </summary>
public class RequestLoggerMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// 构造 请求日志中间件
    /// </summary>
    /// <param name="next"></param>
    public RequestLoggerMiddleware(
        RequestDelegate next
        )
    {
        _next = next;
    }

    /// <summary>
    /// 执行响应流指向新对象
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context)
    {
        if (DHSetting.Current.AllowRequestParams)  // 允许获取则执行
        {
            if (context.Request.Path.Value?.Contains("/api/", StringComparison.OrdinalIgnoreCase) == false)
            {
                // 或请求管道中调用下一个中间件
                await _next(context);
                return;
            }

            if (!DHSetting.Current.ExcludeUrl.IsNullOrWhiteSpace())  // 过滤指定路径
            {
                foreach (var item in DHSetting.Current.ExcludeUrl.Split(','))
                {
                    if (context.Request.Path.Value?.Contains(item, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        // 或请求管道中调用下一个中间件
                        await _next(context);
                        return;
                    }
                }
            }

            XTrace.WriteLine($"Handling request: " + context.Request.Path);

            var api = new ApiRequestInputViewModel
            {
                HttpType = context.Request.Method,
                Query = context.Request.QueryString.Value,
                RequestUrl = context.Request.Path,
                RequestName = "",
                RequestIP = context.Request.Host.Value,
            };

            // 记录请求参数
            await LogRequest(context, api);

            // 拦截响应流
            var originalResponseBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            // 处理请求
            await _next(context);

            // 记录响应详情
            await LogResponse(context, responseBodyStream, api);

            // 重置原始响应体流
            await responseBodyStream.CopyToAsync(originalResponseBodyStream);

            // 响应完成时存入缓存
            context.Response.OnCompleted(() =>
            {
                XTrace.WriteLine($"RequestLog:{DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(0, 10000)}-{api.ElapsedTime}ms-{api.ToJson()}");

                return Task.CompletedTask;
            });
        }
    }

    /// <summary>
    /// 记录请求参数
    /// </summary>
    /// <param name="context"></param>
    /// <param name="api"></param>
    /// <returns></returns>
    private static async Task LogRequest(HttpContext context, ApiRequestInputViewModel api)
    {
        context.Request.EnableBuffering(); // 启用请求体缓冲

        var header = Pool.StringBuilder.Get();
        foreach (var item in context.Request.Headers)
        {
            header.Append($",{item.Key}={item.Value}");
        }
        if (header.Length > 0)
        {
            header = header.Remove(0, 1);
        }
        api.RequestHeader = header.Return(true);

        if (!context.Request.ContentType.IsNullOrWhiteSpace() && context.Request.ContentType.Contains("application/octet-stream"))
        {
            // 记录数据长度
            var contentLength = context.Request.ContentLength ?? 0;

            // 读取并打印十六进制数据
            if (contentLength > 0)
            {
                var buffer = new Byte[contentLength];
                await context.Request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                context.Request.Body.Position = 0;

                var hexString = BitConverter.ToString(buffer).Replace("-", " ");
                api.Body = hexString;
            }
        }
        else
        {
            var request = context.Request;
            var requestBody = String.Empty;

            if (request.ContentLength > 0 && request.ContentType != null)
            {
                using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            api.Body = requestBody;
        }
    }

    /// <summary>
    /// 记录响应详情
    /// </summary>
    /// <param name="context"></param>
    /// <param name="responseBodyStream"></param>
    /// <param name="api"></param>
    /// <returns></returns>
    private static async Task LogResponse(HttpContext context, MemoryStream responseBodyStream, ApiRequestInputViewModel api)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        // 处理下载文件流单独处理
        if (!context.Response.ContentType.IsNullOrWhiteSpace() && context.Response.ContentType.Contains("application/octet-stream"))
        {
            // 读取并打印十六进制数据
            var buffer = responseBodyStream.ToArray();
            var hexString = BitConverter.ToString(buffer).Replace("-", " ");
            api.ResponseBody = hexString;

            // 获取响应数据字节长度
            var contentLength = buffer.Length;
        }
        else
        {
            var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
            api.ResponseBody = responseBody;
        }

        context.Response.Body.Seek(0, SeekOrigin.Begin);
    }
}

/// <summary>
/// 请求日志中间件扩展
/// </summary>
public static class RequestLoggerMiddlewareExtensions
{
    /// <summary>
    /// 使用请求日志中间件
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder app)
    {
        return app == null ? throw new ArgumentNullException(nameof(app)) : app.UseMiddleware<RequestLoggerMiddleware>();
    }
}
