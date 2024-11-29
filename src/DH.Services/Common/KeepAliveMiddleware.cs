using Microsoft.AspNetCore.Http;

using Pek.Webs;

namespace DH.Services.Common;

/// <summary>
/// 表示检查请求是否保持活动的中间件
/// </summary>
public class KeepAliveMiddleware
{
    #region Fields

    private readonly RequestDelegate _next;

    #endregion

    #region Ctor

    public KeepAliveMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    #endregion

    #region Methods

    /// <summary>
    /// 调用中间件操作
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <param name="webHelper">Web帮助程序</param>
    /// <returns>表示异步操作的任务</returns>
    public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context, IWebHelper webHelper)
    {
        // 是否安装了数据库
        if (DHSetting.Current.IsInstalled)
        {
            // 请求的keep-alive页面（我们忽略它以防止创建来宾客户记录）
            var keepAliveUrl = $"{webHelper.GetStoreLocation()}{DHCommonDefaults.KeepAlivePath}";
            if (webHelper.GetThisPageUrl(false).StartsWith(keepAliveUrl, StringComparison.InvariantCultureIgnoreCase))
                return;
        }

        // 或者调用请求管道中的下一个中间件
        await _next(context);
    }

    #endregion
}
