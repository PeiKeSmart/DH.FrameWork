using System.Diagnostics;
using System.Text;

using DH.AspNetCore.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using NewLife;
using NewLife.Collections;
using NewLife.Log;

using Newtonsoft.Json;

namespace DH.AspNetCore.Middleware;

/// <summary>
///  Http 请求中间件
/// </summary>
public class HttpContextMiddleware
{
    private readonly RequestDelegate _next;
    private Stopwatch _stopwatch;

    /// <summary>
    /// 构造 Http 请求中间件
    /// </summary>
    /// <param name="next"></param>
    public HttpContextMiddleware(RequestDelegate next)
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
            if (!context.Request.Path.Value.Contains("/api/", StringComparison.OrdinalIgnoreCase))
            {
                // 或请求管道中调用下一个中间件
                await _next(context);
                return;
            }

            if (!DHSetting.Current.ExcludeUrl.IsNullOrWhiteSpace())  // 过滤指定路径
            {
                foreach (var item in DHSetting.Current.ExcludeUrl.Split(','))
                {
                    if (context.Request.Path.Value.Contains(item, StringComparison.OrdinalIgnoreCase))
                    {
                        await _next(context);
                        return;
                    }
                }
            }

            context.Request.EnableBuffering();  // 可以实现多次读取Body
            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            XTrace.WriteLine($"Handling request: " + context.Request.Path);

            var api = new ApiRequestInputViewModel
            {
                HttpType = context.Request.Method,
                Query = context.Request.QueryString.Value,
                RequestUrl = context.Request.Path,
                RequestName = "",
                RequestIP = context.Request.Host.Value,
            };

            var header = Pool.StringBuilder.Get();
            foreach (var item in context.Request.Headers)
            {
                header.Append($",{item.Key}={item.Value}");
            }
            if (header.Length > 0)
            {
                header = header.Remove(0, 1); // 移除开头的逗号
            }
            api.RequestHeader = header.Return(true);

            using (var newReq = new MemoryStream())
            {
                context.Request.Body.Position = 0; // 确保位置在开始
                await context.Request.Body.CopyToAsync(newReq); // 复制请求体到新流
                newReq.Position = 0; // 重置新请求流的位置
                context.Request.Body = newReq; // 替换请求流

                using (var newRes = new MemoryStream())
                {
                    context.Response.Body = newRes; // 替换响应流

                    using (var reader = new StreamReader(context.Request.Body))
                    {
                        api.Body = await reader.ReadToEndAsync();
                        newReq.Position = 0; // 重置新请求流的位置
                    }

                    // 继续管道处理
                    await _next(context);

                    newRes.Position = 0; // 确保位置在开始
                    using (var reader = new StreamReader(newRes))
                    {
                        api.ResponseBody = await reader.ReadToEndAsync();
                    }

                    newRes.Position = 0; // 确保位置在开始
                    await newRes.CopyToAsync(context.Response.Body); // 将响应写回原始响应流
                }
            }

            // 响应完成时存入缓存
            context.Response.OnCompleted(() =>
            {
                _stopwatch.Stop();
                api.ElapsedTime = _stopwatch.ElapsedMilliseconds;

                XTrace.WriteLine($"RequestLog:{DateTime.Now:yyyyMMddHHmmssfff}-{new Random().Next(0, 10000)}-{api.ElapsedTime}ms-{JsonConvert.SerializeObject(api)}");

                return Task.CompletedTask;
            });

            XTrace.WriteLine($"Finished handling request.{_stopwatch.ElapsedMilliseconds}ms");
        }
        else
        {
            // 或请求管道中调用下一个中间件
            await _next(context);
        }
    }
}

public static class RequestLoggerMiddlewareExtensions {
    public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }
        return app.UseMiddleware<HttpContextMiddleware>();
    }
}
