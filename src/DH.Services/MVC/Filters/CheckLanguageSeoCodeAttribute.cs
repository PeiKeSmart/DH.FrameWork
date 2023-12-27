using DH.Cookies;
using DH.Core;
using DH.Core.Domain.Localization;
using DH.Core.Infrastructure;
using DH.Core.Webs;
using DH.Entity;
using DH.Services.Localization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using NewLife;
using NewLife.Log;
using NewLife.Serialization;

using System.Net;

namespace DH.Services.MVC.Filters;

/// <summary>
/// 表示用于检查多种语言的SEO友好URL的过滤器属性，并在必要时正确重定向
/// </summary>
public sealed class CheckLanguageSeoCodeAttribute : TypeFilterAttribute {
    /// <summary>
    /// 创建过滤器属性的实例
    /// </summary>
    public CheckLanguageSeoCodeAttribute(bool ignore = false) : base(typeof(CheckLanguageSeoCodeFilter))
    {
        IgnoreFilter = ignore;
        Arguments = new object[] { ignore };
    }

    /// <summary>
    /// 得到一个值指示是否忽略过滤操作的执行
    /// </summary>
    public bool IgnoreFilter { get; }

    /// <summary>
    /// 表示一个过滤器，用于检查多种语言的SEO友好URL并在必要时正确重定向
    /// </summary>
    private class CheckLanguageSeoCodeFilter : IAsyncActionFilter {
        private readonly bool _ignoreFilter;
        private readonly IWorkContext _workContext;
        private readonly ICookie DGCookies;

        public CheckLanguageSeoCodeFilter(bool ignoreFilter,
            IWorkContext workContext,
            ICookie cookie)
        {
            _ignoreFilter = ignoreFilter;
            _workContext = workContext;
            DGCookies = cookie;
        }

        /// <summary>
        /// 模型绑定完成后，在操作之前异步调用。
        /// </summary>
        /// <param name="context">动作过滤器的上下文</param>
        /// <param name="next">调用委托以执行下一个动作过滤器或动作本身</param>
        /// <returns>完成时指示过滤器已执行的任务</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await CheckLanguageSeoCodeAsync(context);
            if (context.Result == null)
                await next();
        }

        /// <summary>
        /// 模型绑定完成后，在操作之前异步调用。
        /// </summary>
        /// <param name="context">动作过滤器的上下文</param>
        /// <returns>完成时指示已执行必要的过滤器操作的任务</returns>
        private async Task CheckLanguageSeoCodeAsync(ActionExecutingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.HttpContext.Request == null)
                return;

            var tracer = EngineContext.Current.Resolve<ITracer>();

            using var span = tracer?.NewSpan("CheckLanguageSeoCodeAsync");

            // 仅在GET请求中
            if (!context.HttpContext.Request.Method.Equals(WebRequestMethods.Http.Get, StringComparison.InvariantCultureIgnoreCase))
                return;

            if (!SiteSettingInfo.SiteSettings.IsInstalled)
                return;

            // 如果与默认语言一致，则不跳转
            var lang = Language.FindByDefault();
            if (_workContext.WorkingLanguage.Id == lang.Id)
                return;

            // 检查是否这个过滤器已经覆盖了
            var actionFilter = context.ActionDescriptor.FilterDescriptors
                .Where(filterDescriptor => filterDescriptor.Scope == FilterScope.Action)
                .Select(filterDescriptor => filterDescriptor.Filter)
                .OfType<CheckLanguageSeoCodeAttribute>()
                .FirstOrDefault();

            // 忽略过滤器(一个动作不需要检查)
            if (actionFilter?.IgnoreFilter ?? _ignoreFilter)
                return;

            var localizationSettings = EngineContext.Current.Resolve<LocalizationSettings>();

            // 是否启用SEO友好URL
            if (!localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
                return;

            // 确保此路由已注册且可本地化（RouteProvider中的LocalizedRoute）
            if (context.RouteData.Values["language"] == null)
            {
                if (localizationSettings.DefaultJumpFriendlyUrls)
                {
                    var pageUrl1 = WebHelper2.GetRawUrlStr(context.HttpContext.Request);
                    pageUrl1 = pageUrl1.AddLanguageSeoCodeToUrl(context.HttpContext.Request.PathBase, true, _workContext.WorkingLanguage);
                    context.Result = new LocalRedirectResult(pageUrl1, false);
                }
                else
                {
                    if (_workContext.WorkingLanguage == null || _workContext.WorkingLanguage.UniqueSeoCode.IsNullOrEmpty())
                    {
                        DGCookies.SetValue(DHSetting.Current.LangName, lang.UniqueSeoCode);
                        var pageUrl1 = WebHelper2.GetRawUrlStr(context.HttpContext.Request);
                        pageUrl1 = pageUrl1.AddLanguageSeoCodeToUrl(context.HttpContext.Request.PathBase, true, lang);
                        context.Result = new LocalRedirectResult(pageUrl1, false);
                    }
                }
                return;
            }

            // 检查当前页面URL是否已本地化URL
            var pageUrl = WebHelper2.GetRawUrlStr(context.HttpContext.Request);

            XTrace.WriteLine($"获取当前语言跳转：{pageUrl}");

            var (isLocalized, language) = pageUrl.IsLocalizedUrlAsync(context.HttpContext.Request.PathBase, true);

            XTrace.WriteLine($"获取当前语言跳转11111：{context.HttpContext.Request.PathBase}_{isLocalized}_{language.ToJson()}");

            if (isLocalized && language != null)
                return;

            // 尚未本地化，因此使用工作语言SEO代码重定向到页面
            pageUrl = pageUrl.AddLanguageSeoCodeToUrl(context.HttpContext.Request.PathBase, true, _workContext.WorkingLanguage);

            XTrace.WriteLine($"获取当前语言跳转2222：{pageUrl}");

            context.Result = new LocalRedirectResult(pageUrl, false);

            await Task.FromResult(0);
        }
    }
}