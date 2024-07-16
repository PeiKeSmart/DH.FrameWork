using System.Net;

using DH.AspNetCore.Webs;
using DH.Core.Domain;
using DH.Core.Http;
using DH.Core.Infrastructure;

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

using NewLife;
using NewLife.Collections;
using NewLife.Serialization;

using XCode;
using XCode.Membership;

namespace DH.Core.Webs;

/// <summary>Web助手</summary>
public static class WebHelper2 {
    private static readonly IHttpContextAccessor _httpContextAccessor;
    private static readonly IHostApplicationLifetime _hostApplicationLifetime;
    private static readonly IUrlHelperFactory _urlHelperFactory;
    private static readonly IActionContextAccessor _actionContextAccessor;

    static WebHelper2()
    {
        _httpContextAccessor = EngineContext.Current.Resolve<IHttpContextAccessor>();
        _hostApplicationLifetime = EngineContext.Current.Resolve<IHostApplicationLifetime>();
        _actionContextAccessor = EngineContext.Current.Resolve<IActionContextAccessor>();
        _urlHelperFactory = EngineContext.Current.Resolve<IUrlHelperFactory>();
    }

    #region 兼容处理
    /// <summary>获取请求值</summary>
    /// <param name="request"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static String Get(this HttpRequest request, String key) => request.GetRequestValue(key);

    /// <summary>获取Session值</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="session"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T Get<T>(this ISession session, String key) where T : class
    {
        if (!session.TryGetValue(key, out var buf)) return default(T);

        return buf.ToStr().ToJsonEntity<T>();
    }

    /// <summary>获取Session值</summary>
    /// <param name="session"></param>
    /// <param name="key"></param>
    /// <param name="targetType"></param>
    /// <returns></returns>
    public static Object Get(this ISession session, String key, Type targetType)
    {
        if (!session.TryGetValue(key, out var buf)) return null;

        var rs = buf.ToStr().ToJsonEntity(targetType);
        if (rs is IEntity entity && entity.HasDirty) entity.Dirtys.Clear();

        return rs;
    }

    /// <summary>设置Session值</summary>
    /// <param name="session"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void Set(this ISession session, String key, Object value) => session.Set(key, value?.ToJson().GetBytes());

    /// <summary>获取用户主机</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static String GetUserHost(this HttpContext context)
    {
        var request = context.Request;

        var str = "";
        if (str.IsNullOrEmpty()) str = request.Headers["X-Remote-Ip"];
        if (str.IsNullOrEmpty()) str = request.Headers["HTTP_X_FORWARDED_FOR"];
        if (str.IsNullOrEmpty()) str = request.Headers["X-Real-IP"];
        if (str.IsNullOrEmpty()) str = request.Headers["X-Forwarded-For"];
        if (str.IsNullOrEmpty()) str = request.Headers["REMOTE_ADDR"];
        //if (str.IsNullOrEmpty()) str = request.Headers["Host"];
        if (str.IsNullOrEmpty())
        {
            var addr = context.Connection?.RemoteIpAddress;
            if (addr != null)
            {
                if (addr.IsIPv4MappedToIPv6) addr = addr.MapToIPv4();
                str = addr + "";
            }
        }

        return str;
    }

    /// <summary>返回请求字符串和表单的名值字段，过滤空值和ViewState，同名时优先表单</summary>
    public static IDictionary<String, String> Params
    {
        get
        {
            var ctx = DH.Webs.HttpContext.Current;
            if (ctx.Items["Params"] is IDictionary<String, String> dic) return dic;

            var req = ctx.Request;
            var nvss = new[]
            {
                req.Query,
                req.HasFormContentType ? (IEnumerable<KeyValuePair<String, StringValues>>) req.Form : new List<KeyValuePair<String, StringValues>>()
            };

            // 这里必须用可空字典，否则直接通过索引查不到数据时会抛出异常
            dic = new NullableDictionary<String, String>(StringComparer.OrdinalIgnoreCase);
            foreach (var nvs in nvss)
            {
                foreach (var item in nvs)
                {
                    if (item.Key.IsNullOrWhiteSpace()) continue;
                    if (item.Key.StartsWithIgnoreCase("__VIEWSTATE")) continue;

                    // 空值不需要
                    var value = item.Value.ToString();
                    if (value.IsNullOrWhiteSpace())
                    {
                        // 如果请求字符串里面有值而后面表单为空，则抹去
                        if (dic.ContainsKey(item.Key)) dic.Remove(item.Key);
                        continue;
                    }

                    // 同名时优先表单
                    dic[item.Key] = value.Trim();
                }
            }
            ctx.Items["Params"] = dic;

            return dic;
        }
    }

    /// <summary>获取Linux发行版名称</summary>
    /// <returns></returns>
    public static String GetLinuxName()
    {
        var fr = "/etc/redhat-release";
        var dr = "/etc/debian-release";
        if (File.Exists(fr))
            return File.ReadAllText(fr).Trim();
        else if (File.Exists(dr))
            return File.ReadAllText(dr).Trim();
        else
        {
            var sr = "/etc/os-release";
            if (File.Exists(sr)) return File.ReadAllText(sr).SplitAsDictionary("=", "\n", true)["PRETTY_NAME"].Trim();
        }

        return null;
    }

    /// <summary>获取引用页</summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static String GetReferer(this HttpRequest request) => request.Headers["Referer"].FirstOrDefault();
    #endregion

    /// <summary>修正多租户菜单</summary>
    public static void FixTenantMenu()
    {
        var root = Menu.FindByName("Admin");
        if (root != null)
        {
            var set = DHSetting.Current;
            foreach (var item in root.Childs)
            {
                if (item.Name.Contains("Tenant"))
                {
                    item.Visible = set.EnableTenant;
                    item.Update();
                }
            }
        }
    }

    /// <summary>
    /// 获取当前的HTTP请求协议
    /// </summary>
    public static String CurrentRequestProtocol => IsCurrentConnectionSecured() ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

    /// <summary>
    /// 指示当前连接是否安全
    /// </summary>
    /// <returns>如果受保护，则为true，否则为false</returns>
    public static Boolean IsCurrentConnectionSecured()
    {
        if (!IsRequestAvailable())
            return false;

        var _storeInformationSettings = EngineContext.Current.Resolve<StoreInformationSettings>();

        // 检查主机是否使用负载均衡器
        // 使用HTTP_CLUSTER_HTTPS？
        if (_storeInformationSettings.UseHttpClusterHttps)
            return _httpContextAccessor.HttpContext.Request.Headers[DHHttpDefaults.HttpClusterHttpsHeader].ToString().Equals("on", StringComparison.OrdinalIgnoreCase);

        // 使用HTTP_X_FORWARDED_PROTO？
        if (_storeInformationSettings.UseHttpXForwardedProto)
            return _httpContextAccessor.HttpContext.Request.Headers[DHHttpDefaults.HttpXForwardedProtoHeader].ToString().Equals("https", StringComparison.OrdinalIgnoreCase);

        return _httpContextAccessor.HttpContext.Request.IsHttps;
    }

    /// <summary>
    /// 获取URL引荐来源网址（如果存在）
    /// </summary>
    /// <returns>URL引荐来源</returns>
    public static String GetUrlReferrer()
    {
        if (!IsRequestAvailable())
            return string.Empty;

        // 在某些情况下，URL引荐来源网址为null（例如，在IE 8中）
        return _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Referer];
    }

    /// <summary>
    /// 检查当前HTTP请求是否可用
    /// </summary>
    /// <returns>如果可用，则为true；否则为true。 否则为假</returns>
    public static Boolean IsRequestAvailable()
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
    /// 获取此页面的URL
    /// </summary>
    /// <param name="includeQueryString">指示是否包括查询字符串的值</param>
    /// <param name="useSsl">指示是否获取SSL安全页面URL的值。 传递null以自动确定</param>
    /// <param name="lowercaseUrl">指示是否将小写URL的值</param>
    /// <returns>页面网址</returns>
    public static String GetThisPageUrl(Boolean includeQueryString, Boolean? useSsl = null, Boolean lowercaseUrl = false)
    {
        if (!IsRequestAvailable())
            return String.Empty;

        // 获取网站路径
        var siteLocation = GetSiteLocation(useSsl ?? IsCurrentConnectionSecured());

        // 将本地路径添加到URL
        var pageUrl = $"{siteLocation.TrimEnd('/')}{_httpContextAccessor.HttpContext.Request.Path}";

        // 将查询字符串添加到URL
        if (includeQueryString)
            pageUrl = $"{pageUrl}{_httpContextAccessor.HttpContext.Request.QueryString}";

        // 是否将URL转换为小写
        if (lowercaseUrl)
            pageUrl = pageUrl.ToLowerInvariant();

        return pageUrl;
    }

    /// <summary>
    /// 获取网站路径
    /// </summary>
    /// <param name="useSsl">是否获取SSL安全URL； 传递null以自动确定</param>
    /// <returns>网站路径</returns>
    public static String GetSiteLocation(bool? useSsl = null)
    {
        var storeLocation = String.Empty;

        // 获取站点主机
        var storeHost = GetSiteHost(useSsl ?? IsCurrentConnectionSecured());
        if (!storeHost.IsNullOrEmpty())
        {
            // 添加应用程序路径库（如果存在）
            storeLocation = IsRequestAvailable() ? $"{storeHost.TrimEnd('/')}{_httpContextAccessor.HttpContext.Request.PathBase}" : storeHost;
        }

        // 如果主机为空（仅当HttpContext不可用时才有可能），请使用在管理区域中配置的商店实体的URL
        if (storeHost.IsNullOrEmpty())
        {
            // 不要通过构造函数注入IWorkContext，因为它将导致循环引用

            storeLocation = EngineContext.Current.Resolve<IStoreContext>().CurrentStore?.Url ?? $"{DHSetting.Current.CurDomainUrl}";

            storeLocation = storeLocation ?? throw new Exception("当前站点无法加载");
        }

        // 确保网址以斜杠结尾
        storeLocation = $"{storeLocation.TrimEnd('/')}/";

        return storeLocation;
    }

    /// <summary>
    /// 获取站点主机位置
    /// </summary>
    /// <param name="useSsl">是否获取SSL安全的URL</param>
    /// <returns>Store host location</returns>
    public static String GetSiteHost(bool useSsl)
    {
        if (!IsRequestAvailable())
            return String.Empty;

        // 尝试从请求HOST标头中获取主机
        var hostHeader = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Host];
        if (StringValues.IsNullOrEmpty(hostHeader))
            return String.Empty;

        // 向网址添加方案
        var storeHost = $"{(useSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp)}{Uri.SchemeDelimiter}{hostHeader.FirstOrDefault()}";

        // 确保主机以斜杠结尾
        storeHost = $"{storeHost.TrimEnd('/')}/";

        return storeHost;
    }


    /// <summary>
    /// 修改URL的查询字符串
    /// </summary>
    /// <param name="url">修改网址</param>
    /// <param name="key">查询参数键添加</param>
    /// <param name="values">查询要添加的参数值</param>
    /// <returns>具有传递的查询参数的新URL</returns>
    public static String ModifyQueryString(string url, string key, params string[] values)
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
            uriStr = $"{GetSiteLocation().TrimEnd('/')}{(url.StartsWith(pathBase) ? url.Replace(pathBase, "") : url)}";
        }

        var uri = new Uri(uriStr, UriKind.Absolute);

        // 获取当前查询参数
        var queryParameters = QueryHelpers.ParseQuery(uri.Query);

        // 并添加通过的
        queryParameters[key] = string.Join(",", values);

        // 仅添加第一个值
        // 两个相同的查询参数？ 从理论上讲这是不可能的。
        // 但是MVC对复选框有一些丑陋的实现，我们可以有两个值
        // 在此处找到更多信息：http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
        // 我们进行此验证只是为了确保第一个不被覆盖
        var queryBuilder = new QueryBuilder(queryParameters
            .ToDictionary(parameter => parameter.Key, parameter => parameter.Value.FirstOrDefault()?.ToString() ?? string.Empty));

        // 使用传递的查询参数创建新的URL
        url = $"{(isLocalUrl ? uri.LocalPath : uri.GetLeftPart(UriPartial.Path))}{queryBuilder.ToQueryString()}{uri.Fragment}";

        return url;
    }

    /// <summary>
    /// 从网址中删除查询参数
    /// </summary>
    /// <param name="url">修改网址</param>
    /// <param name="key">查询参数键删除</param>
    /// <param name="value">查询参数值删除； 传递null以使用指定的键删除所有查询参数</param>
    /// <returns>没有传递查询参数的新网址</returns>
    public static String RemoveQueryString(string url, string key, string value = null)
    {
        if (string.IsNullOrEmpty(url))
            return string.Empty;

        if (string.IsNullOrEmpty(key))
            return url;

        // 准备URI对象
        var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
        var isLocalUrl = urlHelper.IsLocalUrl(url);
        var uri = new Uri(isLocalUrl ? $"{GetSiteLocation().TrimEnd('/')}{url}" : url, UriKind.Absolute);

        // 获取当前查询参数
        var queryParameters = QueryHelpers.ParseQuery(uri.Query)
            .SelectMany(parameter => parameter.Value, (parameter, queryValue) => new KeyValuePair<string, string>(parameter.Key, queryValue))
            .ToList();

        if (!string.IsNullOrEmpty(value))
        {
            // 删除特定的查询参数值（如果已通过）
            queryParameters.RemoveAll(parameter => parameter.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)
                && parameter.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }
        else
        {
            // 或通过键删除查询参数
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
    public static T QueryString<T>(string name)
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
    public static void RestartAppDomain()
    {
        _hostApplicationLifetime.StopApplication();
    }

    /// <summary>
    /// 客户端是否被重定向到新位置
    /// </summary>
    public static Boolean IsRequestBeingRedirected
    {
        get
        {
            var response = _httpContextAccessor.HttpContext.Response;

            int[] redirectionStatusCodes = { StatusCodes.Status301MovedPermanently, StatusCodes.Status302Found };
            return redirectionStatusCodes.Contains(response.StatusCode);
        }
    }

    /// <summary>
    /// 是否使用POST将客户端重定向到新位置
    /// </summary>
    public static Boolean IsPostBeingDone
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
    /// 如果请求的资源是引擎不需要处理的典型资源之一，则返回true。
    /// </summary>
    /// <returns>如果请求针对静态资源文件，则为True。</returns>
    public static Boolean IsStaticResource()
    {
        if (!IsRequestAvailable())
            return false;

        string path = _httpContextAccessor.HttpContext.Request.Path;

        var extension = GetExtension(path);
        if (extension == null)
        {
            return false;
        }

        var hashSet = new HashSet<String>();
        hashSet.Add(".js.map");
        if (hashSet.Contains(extension))
        {
            return true;
        }

        // 一些解决方法。 FileExtensionContentTypeProvider包含大多数静态文件扩展名。 所以我们可以使用它
        // 参考: https://github.com/aspnet/StaticFiles/blob/dev/src/Microsoft.AspNetCore.StaticFiles/FileExtensionContentTypeProvider.cs
        // 如果可以返回内容类型，则为静态文件
        var contentTypeProvider = new FileExtensionContentTypeProvider();

        return contentTypeProvider.TryGetContentType(path, out var _);
    }

    /// <summary>
    /// 是否指定了IP地址
    /// </summary>
    /// <param name="address">IP地址</param>
    /// <returns>结果</returns>
    private static Boolean IsIpAddressSet(IPAddress address)
    {
        return address != null && address.ToString() != IPAddress.IPv6Loopback.ToString();
    }

    /// <summary>
    /// 获取指定的HTTP请求URI是否引用本地主机。
    /// </summary>
    /// <param name="req">HTTP请求</param>
    /// <returns>如果HTTP请求URI引用本地主机，则为True</returns>
    public static Boolean IsLocalRequest(HttpRequest req)
    {
        // 参考: https://stackoverflow.com/a/41242493/7860424
        var connection = req.HttpContext.Connection;
        if (IsIpAddressSet(connection.RemoteIpAddress))
        {
            // 我们设置了一个远程地址
            return IsIpAddressSet(connection.LocalIpAddress)
                // 是本地与远程相同，那么我们是本地
                ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                // 否则，如果远程IP地址不是环回地址，则我们是远程的
                : IPAddress.IsLoopback(connection.RemoteIpAddress);
        }

        return true;
    }

    /// <summary>
    /// 获取原始路径和请求的完整查询
    /// </summary>
    /// <param name="request">HTTP请求</param>
    /// <returns>Raw URL</returns>
    public static String GetRawUrlStr(HttpRequest request)
    {
        // 首先尝试从请求功能中获取原始目标
        // 注意：值尚未经过UrlDecoded
        var rawUrl = request.HttpContext.Features.Get<IHttpRequestFeature>()?.RawTarget;

        // 或手动撰写原始网址
        if (rawUrl.IsNullOrEmpty())
            rawUrl = $"{request.PathBase}{request.Path}{request.QueryString}";

        return rawUrl;
    }

    private static String GetExtension(string path)
    {
        // Don't use Path.GetExtension as that may throw an exception if there are
        // invalid characters in the path. Invalid characters should be handled
        // by the FileProviders

        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        int index = path.LastIndexOf('.');
        if (index < 0)
        {
            return null;
        }

        return path.Substring(index);
    }
}