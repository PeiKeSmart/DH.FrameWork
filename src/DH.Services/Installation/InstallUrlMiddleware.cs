using Microsoft.AspNetCore.Http;

using Pek.Webs;

namespace DH.Services.Installation;

/// <summary>
/// 表示中间件，用于检查数据库是否已安装并重定向到中的安装URL
/// </summary>
public class InstallUrlMiddleware
{
    #region Fields

    private readonly RequestDelegate _next;

    #endregion

    #region Ctor

    public InstallUrlMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    #endregion

    #region Methods

    /// <summary>
    /// 调用中间件操作
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <param name="webHelper">Web助手</param>
    /// <returns>表示异步操作的任务</returns>
    public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context, IWebHelper webHelper)
    {
        // 是否安装了数据库
        if (!DHSetting.Current.IsInstalled)
        {
            var installUrl = $"{webHelper.GetStoreLocation()}{DHInstallationDefaults.InstallPath}";
            if (!webHelper.GetThisPageUrl(false).StartsWith(installUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                // 跳转
                context.Response.Redirect(installUrl);
                return;
            }
        }

        // 或者调用请求管道中的下一个中间件
        await _next(context);
    }

    #endregion
}
