using DH.Core.Http;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

using System.Net;

namespace DH.Core
{
    /// <summary>
    /// 表示web助手
    /// </summary>
    public partial class WebHelper : IWebHelper
    {
        #region 字段

        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly Lazy<IStoreContext> _storeContext;

        #endregion

        #region 初始化

        public WebHelper(IActionContextAccessor actionContextAccessor,
            IHostApplicationLifetime hostApplicationLifetime,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelperFactory urlHelperFactory,
            Lazy<IStoreContext> storeContext)
        {
            _actionContextAccessor = actionContextAccessor;
            _hostApplicationLifetime = hostApplicationLifetime;
            _httpContextAccessor = httpContextAccessor;
            _urlHelperFactory = urlHelperFactory;
            _storeContext = storeContext;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 检查当前HTTP请求是否可用
        /// </summary>
        /// <returns>如果可用，则为True；否则为false</returns>
        protected virtual bool IsRequestAvailable()
        {
            if (_httpContextAccessor?.HttpContext == null)
                return false;

            try
            {
                if (_httpContextAccessor.HttpContext.Request == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 是否IP地址格式
        /// </summary>
        /// <param name="address">IP地址</param>
        /// <returns>结果</returns>
        protected virtual bool IsIpAddressSet(IPAddress address)
        {
            var rez = address != null && address.ToString() != IPAddress.IPv6Loopback.ToString();

            return rez;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取URL引用者（如果存在）
        /// </summary>
        /// <returns>URL引用者</returns>
        public virtual string GetUrlReferrer()
        {
            if (!IsRequestAvailable())
                return string.Empty;

            // URL引用在某些情况下为空（例如，在IE 8中）
            return _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Referer];
        }

        /// <summary>
        /// 从HTTP上下文获取IP地址
        /// </summary>
        /// <returns>IP地址字符串</returns>
        public virtual string GetCurrentIpAddress()
        {
            if (!IsRequestAvailable())
                return string.Empty;

            if (_httpContextAccessor.HttpContext.Connection?.RemoteIpAddress is not IPAddress remoteIp)
                return "";

            if (remoteIp.Equals(IPAddress.IPv6Loopback))
                return IPAddress.Loopback.ToString();

            return remoteIp.MapToIPv4().ToString();
        }

        /// <summary>
        /// 获取此网页URL
        /// </summary>
        /// <param name="includeQueryString">指示是否包含查询字符串的值</param>
        /// <param name="useSsl">指示是否获取SSL安全页面URL的值。传递null以自动确定</param>
        /// <param name="lowercaseUrl">指示是否小写URL的值</param>
        /// <returns>页面URL</returns>
        public virtual string GetThisPageUrl(bool includeQueryString, bool? useSsl = null, bool lowercaseUrl = false)
        {
            if (!IsRequestAvailable())
                return string.Empty;

            // 获取商店位置
            var storeLocation = GetStoreLocation(useSsl ?? IsCurrentConnectionSecured());

            // 将本地路径添加到URL
            var pageUrl = $"{storeLocation.TrimEnd('/')}{_httpContextAccessor.HttpContext.Request.Path}";

            // 将查询字符串添加到URL
            if (includeQueryString)
                pageUrl = $"{pageUrl}{_httpContextAccessor.HttpContext.Request.QueryString}";

            // 是否将URL转换为小写
            if (lowercaseUrl)
                pageUrl = pageUrl.ToLowerInvariant();

            return pageUrl;
        }

        /// <summary>
        /// 获取一个值，该值指示当前连接是否受保护
        /// </summary>
        /// <returns>如果安全，则为True，否则为false</returns>
        public virtual bool IsCurrentConnectionSecured()
        {
            if (!IsRequestAvailable())
                return false;

            return _httpContextAccessor.HttpContext.Request.IsHttps;
        }

        /// <summary>
        /// 获取系统主机位置
        /// </summary>
        /// <param name="useSsl">是否获取SSL安全URL</param>
        /// <returns>存储主机位置</returns>
        public virtual string GetStoreHost(bool useSsl)
        {
            if (!IsRequestAvailable())
                return string.Empty;

            // 尝试从请求host标头获取主机
            var hostHeader = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Host];
            if (StringValues.IsNullOrEmpty(hostHeader))
                return string.Empty;

            // 将方案添加到URL
            var storeHost = $"{(useSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp)}{Uri.SchemeDelimiter}{hostHeader.FirstOrDefault()}";

            // 确保主机以斜杠结尾
            storeHost = $"{storeHost.TrimEnd('/')}/";

            return storeHost;
        }

        /// <summary>
        /// 获取系统Url
        /// </summary>
        /// <param name="useSsl">是否获取SSL安全URL；传递null以自动确定</param>
        /// <returns>系统Url</returns>
        public virtual string GetStoreLocation(bool? useSsl = null)
        {
            var storeLocation = string.Empty;

            // 获取系统Host
            var storeHost = GetStoreHost(useSsl ?? IsCurrentConnectionSecured());
            if (!string.IsNullOrEmpty(storeHost))
            {
                // 添加应用程序路径库（如果存在）
                storeLocation = IsRequestAvailable() ? $"{storeHost.TrimEnd('/')}{_httpContextAccessor.HttpContext.Request.PathBase}" : storeHost;
            }

            // 如果主机为空（仅当HttpContext不可用时才可能），请使用在管理区域中配置的存储实体的URL
            if (string.IsNullOrEmpty(storeHost))
                storeLocation = _storeContext.Value.CurrentStore?.Url
                                ?? throw new Exception("Current store cannot be loaded");

            // 确保URL以斜杠结尾
            storeLocation = $"{storeLocation.TrimEnd('/')}/";

            return storeLocation;
        }

        /// <summary>
        /// 如果请求的资源是cms引擎不需要处理的典型资源之一，则返回true。
        /// </summary>
        /// <returns>如果请求以静态资源文件为目标，则为True。</returns>
        public virtual bool IsStaticResource()
        {
            if (!IsRequestAvailable())
                return false;

            string path = _httpContextAccessor.HttpContext.Request.Path;

            // 一个小的变通办法。FileExtensionContentTypeProvider包含大多数静态文件扩展名。所以我们可以用它
            // 来源：https://github.com/aspnet/StaticFiles/blob/dev/src/Microsoft.AspNetCore.StaticFiles/FileExtensionContentTypeProvider.cs
            // 如果它可以返回内容类型，那么它是一个静态文件
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            return contentTypeProvider.TryGetContentType(path, out var _);
        }

        /// <summary>
        /// 修改URL的查询字符串
        /// </summary>
        /// <param name="url">要修改的url</param>
        /// <param name="key">查询要添加的参数键</param>
        /// <param name="values">查询要添加的参数值</param>
        /// <returns>带有传递的查询参数的新URL</returns>
        public virtual string ModifyQueryString(string url, string key, params string[] values)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            if (string.IsNullOrEmpty(key))
                return url;

            // 准备URI对象
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            var isLocalUrl = urlHelper.IsLocalUrl(url);

            var uriStr = url;
            if (isLocalUrl)
            {
                var pathBase = _httpContextAccessor.HttpContext.Request.PathBase;
                uriStr = $"{GetStoreLocation().TrimEnd('/')}{(url.StartsWith(pathBase) ? url.Replace(pathBase, "") : url)}";
            }

            var uri = new Uri(uriStr, UriKind.Absolute);

            // 获取当前查询参数
            var queryParameters = QueryHelpers.ParseQuery(uri.Query);

            // 并添加传递的一个
            queryParameters[key] = string.Join(",", values);

            // 仅添加第一个值
            // 两个相同的查询参数？理论上这是不可能的。
            // 但是MVC有一些丑陋的复选框实现，我们可以有两个值
            // 在此处查找更多信息： http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
            // we do this validation just to ensure that the first one is not overridden
            var queryBuilder = new QueryBuilder(queryParameters
                .ToDictionary(parameter => parameter.Key, parameter => parameter.Value.FirstOrDefault()?.ToString() ?? string.Empty));

            // 使用传递的查询参数创建新URL
            url = $"{(isLocalUrl ? uri.LocalPath : uri.GetLeftPart(UriPartial.Path))}{queryBuilder.ToQueryString()}{uri.Fragment}";

            return url;
        }

        /// <summary>
        /// 从URL中删除查询参数
        /// </summary>
        /// <param name="url">要修改的url</param>
        /// <param name="key">查询要删除的参数键</param>
        /// <param name="value">查询要删除的参数值；传递null以删除具有指定键的所有查询参数</param>
        /// <returns>没有传递查询参数的新URL</returns>
        public virtual string RemoveQueryString(string url, string key, string value = null)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            if (string.IsNullOrEmpty(key))
                return url;

            // 准备URI对象
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            var isLocalUrl = urlHelper.IsLocalUrl(url);
            var uri = new Uri(isLocalUrl ? $"{GetStoreLocation().TrimEnd('/')}{url}" : url, UriKind.Absolute);

            // 获取当前查询参数
            var queryParameters = QueryHelpers.ParseQuery(uri.Query)
                .SelectMany(parameter => parameter.Value, (parameter, queryValue) => new KeyValuePair<string, string>(parameter.Key, queryValue))
                .ToList();

            if (!string.IsNullOrEmpty(value))
            {
                // 如果传递了特定的查询参数值，则将其删除
                queryParameters.RemoveAll(parameter => parameter.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)
                    && parameter.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {
                // 或按键删除查询参数
                queryParameters.RemoveAll(parameter => parameter.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            }

            var queryBuilder = new QueryBuilder(queryParameters);

            // 创建没有传递查询参数的新URL
            url = $"{(isLocalUrl ? uri.LocalPath : uri.GetLeftPart(UriPartial.Path))}{queryBuilder.ToQueryString()}{uri.Fragment}";

            return url;
        }

        /// <summary>
        /// 按名称获取查询字符串值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="name">查询参数名称</param>
        /// <returns>查询字符串值</returns>
        public virtual T QueryString<T>(string name)
        {
            if (!IsRequestAvailable())
                return default;

            if (StringValues.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Query[name]))
                return default;

            return CommonHelper.To<T>(_httpContextAccessor.HttpContext.Request.Query[name].ToString());
        }

        /// <summary>
        /// 重新启动应用程序域
        /// </summary>
        public virtual void RestartAppDomain()
        {
            _hostApplicationLifetime.StopApplication();
        }

        /// <summary>
        /// 获取一个值，该值指示客户端是否正在重定向到新位置
        /// </summary>
        public virtual bool IsRequestBeingRedirected
        {
            get
            {
                var response = _httpContextAccessor.HttpContext.Response;
                //ASP.NET 4风格-返回response.IsRequestBeingRedirected；
                int[] redirectionStatusCodes = { StatusCodes.Status301MovedPermanently, StatusCodes.Status302Found };

                return redirectionStatusCodes.Contains(response.StatusCode);
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示客户端是否正在使用POST重定向到新位置
        /// </summary>
        public virtual bool IsPostBeingDone
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Items[DHHttpDefaults.IsPostBeingDoneRequestItem] == null)
                    return false;

                return Convert.ToBoolean(_httpContextAccessor.HttpContext.Items[DHHttpDefaults.IsPostBeingDoneRequestItem]);
            }

            set => _httpContextAccessor.HttpContext.Items[DHHttpDefaults.IsPostBeingDoneRequestItem] = value;
        }

        /// <summary>
        /// 获取当前HTTP请求协议
        /// </summary>
        public virtual string GetCurrentRequestProtocol()
        {
            return IsCurrentConnectionSecured() ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;
        }

        /// <summary>
        /// 获取指定的HTTP请求URI是否引用本地主机。
        /// </summary>
        /// <param name="req">HTTP请求</param>
        /// <returns>True，如果HTTP请求URI引用本地主机</returns>
        public virtual bool IsLocalRequest(HttpRequest req)
        {
            // 来源：https://stackoverflow.com/a/41242493/7860424
            var connection = req.HttpContext.Connection;
            if (IsIpAddressSet(connection.RemoteIpAddress))
            {
                // 我们设置了远程地址
                return IsIpAddressSet(connection.LocalIpAddress)
                    // 本地和远程是一样的，那么我们是本地的
                    ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                    // 否则，如果远程IP地址不是环回地址，则我们是远程的
                    : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            return true;
        }

        /// <summary>
        /// 获取请求的原始路径和完整查询
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <returns>原始URL</returns>
        public virtual string GetRawUrl(HttpRequest request)
        {
            // 首先尝试从请求功能获取原始目标
            // 注意：值尚未UrlDecoded
            var rawUrl = request.HttpContext.Features.Get<IHttpRequestFeature>()?.RawTarget;

            //or compose raw URL manually
            if (string.IsNullOrEmpty(rawUrl))
                rawUrl = $"{request.PathBase}{request.Path}{request.QueryString}";

            return rawUrl;
        }

        /// <summary>
        /// 获取是否使用AJAX发出请求
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <returns>结果</returns>
        public virtual bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Headers == null)
                return false;

            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        #endregion
    }
}
