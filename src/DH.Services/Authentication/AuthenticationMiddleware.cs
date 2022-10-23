using DH.Core;
using DH.Core.Configuration;
using DH.Core.Domain.Customers;
using DH.Core.Infrastructure;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using NewLife.Log;

using XCode.Membership;

namespace DH.Services.Authentication
{
    /// <summary>
    /// 表示启用身份验证的中间件
    /// </summary>
    public class AuthenticationMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        #region Ctor

        public AuthenticationMiddleware(IAuthenticationSchemeProvider schemes, RequestDelegate next)
        {
            Schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 调用中间件操作
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>表示异步操作的任务</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            context.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
            {
                OriginalPath = context.Request.Path,
                OriginalPathBase = context.Request.PathBase
            });

            // 给任何IAuthenticationRequestHandler方案一个处理请求的机会
            var handlers = EngineContext.Current.Resolve<IAuthenticationHandlerProvider>();
            foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
            {
                try
                {
                    if (await handlers.GetHandlerAsync(context, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                        return;
                }
                catch (Exception ex)
                {
                    if (!DHSetting.Current.IsInstalled)
                        continue;

                    var externalAuthenticationSettings =
                        EngineContext.Current.Resolve<ExternalAuthenticationSettings>();

                    if (!externalAuthenticationSettings.LogErrors)
                        continue;

                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();

                    // 获取当前客户
                    var currentCustomer = await EngineContext.Current.Resolve<IWorkContext>().GetCurrentCustomerAsync();

                    XTrace.WriteException(ex);
                    LogProvider.Provider?.WriteLog("系统", "错误", false, ex.Message + " " + Environment.NewLine + ex.GetMessage(), currentCustomer.ID, currentCustomer.Name, webHelper.GetCurrentIpAddress());
                }
            }

            var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null)
            {
                var result = await context.AuthenticateAsync(defaultAuthenticate.Name);
                if (result?.Principal != null)
                {
                    context.User = result.Principal;
                }
            }

            await _next(context);
        }

        #endregion
    }
}
