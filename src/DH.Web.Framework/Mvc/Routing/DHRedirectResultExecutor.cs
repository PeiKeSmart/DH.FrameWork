using DH.Core;
using DH.Core.Domain.Security;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;

using System.Net;

namespace DH.Web.Framework.Mvc.Routing
{
    /// <summary>
    /// 表示自定义重写的重定向结果执行器
    /// </summary>
    public partial class DHRedirectResultExecutor : RedirectResultExecutor
    {
        #region Fields

        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly SecuritySettings _securitySettings;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public DHRedirectResultExecutor(IActionContextAccessor actionContextAccessor,
            ILoggerFactory loggerFactory,
            IUrlHelperFactory urlHelperFactory,
            SecuritySettings securitySettings,
            IWebHelper webHelper) : base(loggerFactory, urlHelperFactory)
        {
            _actionContextAccessor = actionContextAccessor;
            _urlHelperFactory = urlHelperFactory;
            _securitySettings = securitySettings;
            _webHelper = webHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 执行传递的重定向结果
        /// </summary>
        /// <param name="context">Action上下文</param>
        /// <param name="result">重定向结果</param>
        /// <returns>表示异步操作的任务</returns>
        public override Task ExecuteAsync(ActionContext context, RedirectResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            if (_securitySettings.AllowNonAsciiCharactersInHeaders)
            {
                // 传递的重定向URL可能包含现在不允许的非ASCII字符 (看 https://github.com/aspnet/KestrelHttpServer/issues/1144)
                // 所以我们在处理之前强制对这个URL进行编码
                var url = WebUtility.UrlDecode(result.Url);
                var urlHelper = result.UrlHelper ?? _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
                var isLocalUrl = urlHelper.IsLocalUrl(url);

                var uri = new Uri(isLocalUrl ? $"{_webHelper.GetStoreLocation().TrimEnd('/')}{url}" : url, UriKind.Absolute);

                // 允许将URI方案重定向到http和https
                if ((uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps) && urlHelper.IsLocalUrl(uri.AbsolutePath))
                    result.Url = isLocalUrl ? uri.PathAndQuery : $"{uri.GetLeftPart(UriPartial.Query)}{uri.Fragment}";
                else
                    result.Url = urlHelper.RouteUrl("Homepage");
            }

            return base.ExecuteAsync(context, result);
        }

        #endregion
    }
}
