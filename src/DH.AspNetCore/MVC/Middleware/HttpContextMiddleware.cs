using DH.AspNetCore.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using NewLife;
using NewLife.Collections;
using NewLife.Log;

using Newtonsoft.Json;

using System.Diagnostics;

namespace DH.AspNetCore.Middleware;

/// <summary>
///  Http 请求中间件
/// </summary>
public class HttpContextMiddleware {
    private readonly RequestDelegate _next;

    private Stopwatch _stopwatch;

    /// <summary>
    /// 构造 Http 请求中间件
    /// </summary>
    /// <param name="next"></param>
    public HttpContextMiddleware(
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
            //context.Request.EnableRewind();
            context.Request.EnableBuffering();  // 可以实现多次读取Body

            if (context.Request.Path.Value.Contains("/notify-hub", StringComparison.OrdinalIgnoreCase) || context.Request.Path.Value.Contains("/CaptCha", StringComparison.OrdinalIgnoreCase))
            {
                // 或请求管道中调用下一个中间件
                await _next(context);
                return;
            }

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
            foreach(var item in context.Request.Headers)
            {
                header.Append($",{item.Key}={item.Value}");
            }
            if (header.Length > 0)
            {
                header = header.Remove(0, 1);
            }
            api.RequestHeader = header.Put(true);

            var reqOrigin = context.Request.Body;
            var resOrigin = context.Response.Body;
            try
            {
                using (var newReq = new MemoryStream())
                {
                    //替换request流
                    context.Request.Body = newReq;
                    using (var newRes = new MemoryStream())
                    {
                        //替换response流
                        context.Response.Body = newRes;
                        using (var reader = new StreamReader(reqOrigin))
                        {
                            //读取原始请求流的内容
                            api.Body = reader.ReadToEnd();
                        }
                        using (var writer = new StreamWriter(newReq))
                        {
                            writer.Write(api.Body);
                            writer.Flush();
                            newReq.Position = 0;
                            await _next(context);
                        }

                        using (var reader = new StreamReader(newRes))
                        {
                            newRes.Position = 0;
                            api.ResponseBody = reader.ReadToEnd();
                        }
                        using (var writer = new StreamWriter(resOrigin))
                        {
                            writer.Write(api.ResponseBody);
                        }
                    }
                }
            }
            finally
            {
                context.Request.Body = reqOrigin;
                context.Response.Body = resOrigin;
            }

            // 响应完成时存入缓存
            context.Response.OnCompleted(() =>
            {
                _stopwatch.Stop();
                api.ElapsedTime = _stopwatch.ElapsedMilliseconds;
                //api.LogType = ApiRequestInputViewModel.LOG_TYPE.BD;
                //api.LogResult = ApiRequestInputViewModel.LOG_RESULT.S;
                //if (api.ResponseBody.ToLower().Contains("errmsg"))
                //    api.LogBusiResult = ApiRequestInputViewModel.LOG_BUSI_RESULT.F;
                //else
                //    api.LogBusiResult = ApiRequestInputViewModel.LOG_BUSI_RESULT.S;

                //if (!((bool)api.RequestUrl?.Contains("PushApiLogger")))
                //    _cacheService.Set<string>($"RequestLog:{DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(0, 10000)}-{api.ElapsedTime}ms", $"{JsonConvert.SerializeObject(api)}");

                XTrace.WriteLine($"RequestLog:{DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(0, 10000)}-{api.ElapsedTime}ms-{JsonConvert.SerializeObject(api)}");

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
