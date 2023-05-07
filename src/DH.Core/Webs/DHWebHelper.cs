using DH.AspNetCore.Webs;
using DH.Core.Infrastructure;
using DH.Exceptions;
using DH.Helpers;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

using NewLife;
using NewLife.Collections;

using System.Text;
using System.Web;

namespace DH.Webs;

/// <summary>网页工具类</summary>
public static partial class DHWebHelper
{
    #region Http请求
    /// <summary>返回请求字符串和表单的名值字段，过滤空值和ViewState，同名时优先表单</summary>
    public static IDictionary<String, String> Params
    {
        get
        {
            var ctx = DHWeb.HttpContext;
            if (ctx.Items["Params"] is IDictionary<String, String> dic) return dic;

            var req = ctx.Request;
            // 这里必须用可空字典，否则直接通过索引查不到数据时会抛出异常
            dic = new NullableDictionary<String, String>(StringComparer.OrdinalIgnoreCase);
            //foreach (var item in req.RouteValues)
            //{
            //    if (item.Value != null) dic[item.Key] = item.Value as String;
            //}

            IEnumerable<KeyValuePair<String, StringValues>>[] nvss;
            nvss = req.HasFormContentType ?
                new IEnumerable<KeyValuePair<String, StringValues>>[] { req.Query, req.Form } :
                new IEnumerable<KeyValuePair<String, StringValues>>[] { req.Query };

            foreach (var nvs in nvss)
            {
                foreach (var kv in nvs)
                {
                    var item = kv.Key;
                    if (item.IsNullOrWhiteSpace()) continue;
                    if (item.StartsWithIgnoreCase("__VIEWSTATE")) continue;

                    // 空值不需要
                    var value = kv.Value;
                    if (value.Count == 0)
                    {
                        // 如果请求字符串里面有值而后面表单为空，则抹去
                        if (dic.ContainsKey(item)) dic.Remove(item);
                        continue;
                    }

                    // 同名时优先表单
                    dic[item] = value.ToString().Trim();
                }
            }

            // 尝试从body读取json格式的参数
            if (req.GetRequestBody<Object>() is NullableDictionary<String, Object> entityBody)
            {
                foreach (var (key, value) in entityBody)
                {
                    var v = value?.ToString()?.Trim();
                    if (v.IsNullOrWhiteSpace()) continue;
                    dic[key] = v;
                }
            }

            ctx.Items["Params"] = dic;

            return dic;
        }
    }

    /// <summary>保存上传文件</summary>
    /// <param name="file"></param>
    /// <param name="filename"></param>
    public static void SaveAs(this IFormFile file, String filename)
    {
        using var fs = new FileStream(filename, FileMode.OpenOrCreate);
        //file.OpenReadStream().CopyTo(fs);
        file.CopyTo(fs);
        fs.SetLength(fs.Position);
    }
    #endregion

    #region Url扩展
    /// <summary>追加Url参数，不为空时加与符号</summary>
    /// <param name="sb"></param>
    /// <param name="str"></param>
    /// <returns></returns>
    public static StringBuilder UrlParam(this StringBuilder sb, String str)
    {
        if (str.IsNullOrWhiteSpace()) return sb;

        if (sb.Length > 0)
            sb.Append("&");
        //else
        //    sb.Append("?");

        sb.Append(str);

        return sb;
    }

    /// <summary>追加Url参数，不为空时加与符号</summary>
    /// <param name="sb">字符串构建</param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static StringBuilder UrlParam(this StringBuilder sb, String name, Object value)
    {
        if (name.IsNullOrWhiteSpace()) return sb;

        // 必须注意，value可能是时间类型
        var val = value is DateTime dt ? dt.ToFullString() : value + "";
        return UrlParam(sb, $"{HttpUtility.UrlEncode(name)}={HttpUtility.UrlEncode(val)}");
    }

    /// <summary>把一个参数字典追加Url参数，指定包含的参数</summary>
    /// <param name="sb">字符串构建</param>
    /// <param name="pms">参数字典</param>
    /// <param name="includes">包含的参数</param>
    /// <returns></returns>
    public static StringBuilder UrlParams(this StringBuilder sb, IDictionary<String, String> pms, params String[] includes)
    {
        foreach (var item in pms)
        {
            if (!item.Value.IsNullOrEmpty() && item.Key.EqualIgnoreCase(includes))
                sb.UrlParam(item.Key, item.Value);
        }
        return sb;
    }

    /// <summary>把一个参数字典追加Url参数，排除一些参数</summary>
    /// <param name="sb">字符串构建</param>
    /// <param name="pms">参数字典</param>
    /// <param name="excludes">要排除的参数</param>
    /// <returns></returns>
    public static StringBuilder UrlParamsExcept(this StringBuilder sb, IDictionary<String, String> pms, params String[] excludes)
    {
        foreach (var item in pms)
        {
            if (!item.Value.IsNullOrEmpty() && !item.Key.EqualIgnoreCase(excludes))
                sb.UrlParam(item.Key, item.Value);
        }
        return sb;
    }

    /// <summary>相对路径转Uri</summary>
    /// <param name="url">相对路径</param>
    /// <param name="baseUri">基础</param>
    /// <returns></returns>
    public static Uri AsUri(this String url, Uri baseUri = null)
    {
        if (url.IsNullOrEmpty()) return null;

        if (url.StartsWith("~/")) url = "/" + url.Substring(2);
        else
        {
            url = url.UrlDecode();
            if (url.StartsWith("~/")) url = "/" + url.Substring(2);
        }

        // 绝对路径
        if (!url.StartsWith("/")) return new Uri(url);

        // 相对路径
        if (baseUri == null) throw new ArgumentNullException(nameof(baseUri));
        return new Uri(baseUri, url);
    }

    /// <summary>打包返回地址</summary>
    /// <param name="uri"></param>
    /// <param name="returnUrl"></param>
    /// <param name="returnKey"></param>
    /// <returns></returns>
    public static Uri AppendReturn(this Uri uri, String returnUrl, String returnKey = null)
    {
        if (uri == null || returnUrl.IsNullOrEmpty()) return uri;

        if (returnKey.IsNullOrEmpty()) returnKey = "r";

        // 如果协议和主机相同，则削减为只要路径查询部分
        if (returnUrl.StartsWithIgnoreCase("http"))
        {
            var ruri = new Uri(returnUrl);
            if (ruri.Scheme.EqualIgnoreCase(uri.Scheme) && ruri.Host.EqualIgnoreCase(uri.Host)) returnUrl = ruri.PathAndQuery;
        }

        var url = uri + "";
        if (url.Contains("?"))
            url += "&";
        else
            url += "?";
        url += returnKey + "=" + HttpUtility.UrlEncode(returnUrl);

        return new Uri(url);
    }

    /// <summary>打包返回地址</summary>
    /// <param name="url"></param>
    /// <param name="returnUrl"></param>
    /// <param name="returnKey"></param>
    /// <returns></returns>
    public static String AppendReturn(this String url, String returnUrl, String returnKey = null)
    {
        if (url.IsNullOrEmpty() || returnUrl.IsNullOrEmpty()) return url;

        if (returnKey.IsNullOrEmpty()) returnKey = "r";

        // 如果协议和主机相同，则削减为只要路径查询部分
        if (url.StartsWithIgnoreCase("http") && returnUrl.StartsWithIgnoreCase("http"))
        {
            var uri = new Uri(url);
            var ruri = new Uri(returnUrl);
            if (ruri.Scheme.EqualIgnoreCase(uri.Scheme) && ruri.Authority.EqualIgnoreCase(uri.Authority)) returnUrl = ruri.PathAndQuery;
        }

        if (url.Contains("?"))
            url += "&";
        else
            url += "?";
        //url += returnKey + "=" + returnUrl;
        url += returnKey + "=" + HttpUtility.UrlEncode(returnUrl);

        return url;
    }
    #endregion

    #region 工具方法

    /// <summary>
    /// 重新启动应用程序域
    /// </summary>
    /// <param name="makeRedirect">一个值，该值指示我们是否应在重新启动后进行重定向</param>
    public static void RestartAppDomain(bool makeRedirect = false)
    {
        var _hostApplicationLifetime = EngineContext.Current.Resolve<IHostApplicationLifetime>();

        // 网站将在下一个请求期间自动重新启动
        // 调整web.config强制重新启动
        var success = TryWriteWebConfig();
        if (!success)
        {
            throw new DHException("needs to be restarted due to a configuration change, but was unable to do so." + Environment.NewLine +
                "To prevent this issue in the future, a change to the web server configuration is required:" + Environment.NewLine +
                "- run the application in a full trust environment, or" + Environment.NewLine +
                "- give the application write access to the 'web.config' file.");
        }

        if (Environment.OSVersion.Platform == PlatformID.Unix)
            _hostApplicationLifetime.StopApplication();
    }

    /// <summary>
    /// 尝试编写web.config文件
    /// </summary>
    /// <returns></returns>
    public static Boolean TryWriteWebConfig()
    {
        var _fileProvider = EngineContext.Current.Resolve<IDHFileProvider>();
        try
        {
            _fileProvider.SetLastWriteTimeUtc(_fileProvider.MapPath(DHInfrastructureDefaults.WebConfigPath), DateTime.UtcNow);
            return true;
        }
        catch
        {
            return false;
        }
    }

    #endregion
}