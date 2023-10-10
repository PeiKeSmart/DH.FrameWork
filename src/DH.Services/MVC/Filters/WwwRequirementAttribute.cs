using DH.Core;
using DH.Core.Domain.Seo;
using DH.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Net;

namespace DH.Services.MVC.Filters;

/// <summary>
/// 表示一个筛选器属性，该属性在 URL 的开头检查 WWW，并在必要时正确重定向
/// </summary>
public sealed class WwwRequirementAttribute : TypeFilterAttribute {
    #region Ctor

    /// <summary>
    /// 创建筛选器属性的实例
    /// </summary>
    public WwwRequirementAttribute() : base(typeof(WwwRequirementFilter))
    {
    }

    #endregion

    #region Nested filter

    /// <summary>
    /// 表示一个筛选器，该筛选器在 URL 的开头检查 WWW，并在必要时正确重定向
    /// </summary>
    private class WwwRequirementFilter : IAsyncAuthorizationFilter {
        #region Fields

        protected readonly IWebHelper _webHelper;
        protected readonly SeoSettings _seoSettings;

        #endregion

        #region Ctor

        public WwwRequirementFilter(IWebHelper webHelper,
            SeoSettings seoSettings)
        {
            _webHelper = webHelper;
            _seoSettings = seoSettings;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 检查 URL 开头的 WWW 前缀，并在必要时正确重定向
        /// </summary>
        /// <param name="context">授权筛选器上下文</param>
        /// <param name="withWww">URL 是否必须以 WWW 开头</param>
        private void RedirectRequest(AuthorizationFilterContext context, bool withWww)
        {
            // 根据安全连接获取方案
            var urlScheme = $"{_webHelper.GetCurrentRequestProtocol()}{Uri.SchemeDelimiter}";

            // 使用 WWW 撰写 URL 的开头
            var urlWith3W = $"{urlScheme}www.";

            // 获取请求的网址
            var currentUrl = _webHelper.GetThisPageUrl(true);

            // 请求的网址是否以 WWW 开头
            var urlStartsWith3W = currentUrl.StartsWith(urlWith3W, StringComparison.OrdinalIgnoreCase);

            // 页面应具有 WWW 前缀，因此将 301（永久）重定向设置为 WWW 的 URL
            if (withWww && !urlStartsWith3W)
                context.Result = new RedirectResult(currentUrl.Replace(urlScheme, urlWith3W), true);

            // 页面不应具有 WWW 前缀，因此将 301（永久）重定向设置为不带 WWW 的 URL
            if (!withWww && urlStartsWith3W)
                context.Result = new RedirectResult(currentUrl.Replace(urlWith3W, urlScheme), true);
        }

        /// <summary>
        /// 在筛选器管道中提前调用以确认请求已获得授权
        /// </summary>
        /// <param name="context">授权筛选器上下文</param>
        private void CheckWwwRequirement(AuthorizationFilterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // 仅在 GET 请求中，否则浏览器可能无法正确传播动词和请求正文。
            if (!context.HttpContext.Request.Method.Equals(WebRequestMethods.Http.Get, StringComparison.InvariantCultureIgnoreCase))
                return;

            if (!DHSetting.Current.IsInstalled)
                return;

            // 对于本地主机，请忽略此规则
            if (_webHelper.IsLocalRequest(context.HttpContext.Request))
                return;

            switch (_seoSettings.WwwRequirement)
            {
                case WwwRequirement.WithWww:
                    // 重定向至带有起始 WWW 的网址
                    RedirectRequest(context, true);
                    break;

                case WwwRequirement.WithoutWww:
                    // 重定向到不启动 WWW 的 URL
                    RedirectRequest(context, false);
                    break;

                case WwwRequirement.NoMatter:
                    // 什么都不做
                    break;

                default:
                    throw new DHException("不支持的 WwwRequirement 参数");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 在筛选器管道中提前调用以确认请求已获得授权
        /// </summary>
        /// <param name="context">授权筛选器上下文</param>
        /// <returns>表示异步操作的任务</returns>
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            CheckWwwRequirement(context);
            return Task.CompletedTask;
        }

        #endregion
    }

    #endregion
}