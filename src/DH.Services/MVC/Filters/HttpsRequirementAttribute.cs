using DH.Core;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

using NewLife;

using System.Net;

namespace DH.Services.MVC.Filters;

/// <summary>
/// 检查当前连接是否安全，并在必要时正确重定向。
/// Linux下会因为反向代理导致存在问题
/// </summary>
public sealed class HttpsRequirementAttribute : TypeFilterAttribute {
    /// <summary>
    /// 创建过滤器属性的实例
    /// </summary>
    /// <param name="ignore">是否忽略过滤操作的执行</param>
    /// <param name="pageType">页面类型</param>
    public HttpsRequirementAttribute(bool ignore = false, string pageType = "") : base(typeof(HttpsRequirementFilter))
    {
        IgnoreFilter = ignore;
        PageType = pageType;
        Arguments = new object[] { ignore, pageType };
    }

    /// <summary>
    /// 得到一个值指示是否忽略过滤操作的执行
    /// </summary>
    public bool IgnoreFilter { get; }

    /// <summary>
    /// 页面类型
    /// </summary>
    public String? PageType { get; }

    /// <summary>
    /// 确认检查当前连接是否安全并在必要时正确重定向
    /// </summary>
    private class HttpsRequirementFilter : IAsyncAuthorizationFilter {
        private readonly bool _ignoreFilter;
        private readonly string _pageType;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IWebHelper _webHelper;

        public HttpsRequirementFilter(bool ignoreFilter, String pageType, IWebHelper webHelper, IWebHostEnvironment webHostEnvironment)
        {
            _ignoreFilter = ignoreFilter;
            _pageType = pageType;
            _webHostEnvironment = webHostEnvironment;
            _webHelper = webHelper;
        }

        /// <summary>
        /// 在过滤器管道中提前调用以确认请求已被授权
        /// </summary>
        /// <param name="context">授权过滤器上下文</param>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await CheckHttpsRequirementAsync(context);
        }

        /// <summary>
        /// 在过滤器管道中早期调用以确认请求已授权
        /// </summary>
        /// <param name="context">授权过滤器上下文</param>
        /// <returns>完成时指示过滤器已执行的任务</returns>
        private async Task CheckHttpsRequirementAsync(AuthorizationFilterContext context)
        {
            if (DHSetting.Current.AllSslEnabled) // 如果启用全站则全局起作用，此处无效
                return;

            if (DHSetting.Current.SslEnabled == 0)  // 如果为不处理，此处无效
                return;

            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.HttpContext.Request == null)
                return;

            // 仅在GET请求中，否则浏览器可能无法正确传播动词和请求主体
            if (!context.HttpContext.Request.Method.Equals(WebRequestMethods.Http.Get, StringComparison.InvariantCultureIgnoreCase))
                return;

            if (!DHSetting.Current.IsInstalled)
                return;

            if (_pageType.IsNullOrWhiteSpace())
                return;

            if (!DHSetting.Current.SslPageType.Contains(_pageType, StringComparison.OrdinalIgnoreCase))
                return;

            // 检查是否已为操作覆盖此筛选器
            var actionFilter = context.ActionDescriptor.FilterDescriptors
                .Where(filterDescriptor => filterDescriptor.Scope == FilterScope.Action)
                .Select(filterDescriptor => filterDescriptor.Filter)
                .OfType<HttpsRequirementAttribute>()
                .FirstOrDefault();

            if (actionFilter?.IgnoreFilter ?? _ignoreFilter)
                return;

            // 缓存的连接可能会导致不稳定的行为在开发环境中,当我们使用永久重定向
            var isPermanent = !_webHostEnvironment.IsDevelopment();

            // 当前连接是否安全
            var currentConnectionSecured = _webHelper.IsCurrentConnectionSecured();

            // 页面应该是安全的，因此重定向（永久）到页面的HTTPS版本
            if (DHSetting.Current.SslEnabled == 1 && !currentConnectionSecured)
                context.Result = new RedirectResult(_webHelper.GetThisPageUrl(true, true), isPermanent);

            // 页面不应该受到保护，因此（永久）重定向到页面的HTTP版本
            if (DHSetting.Current.SslEnabled == 2 && currentConnectionSecured)
                context.Result = new RedirectResult(_webHelper.GetThisPageUrl(true, false), isPermanent);

            await Task.FromResult(0);
        }
    }
}