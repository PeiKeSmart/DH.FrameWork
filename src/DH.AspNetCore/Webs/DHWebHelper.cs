﻿using System.Text;
using System.Web;

using DH.AspNetCore.Webs;
using DH.Helpers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;

using NewLife;
using NewLife.Collections;
using NewLife.Log;
using NewLife.Security;

namespace DH.Webs;

/// <summary>网页工具类</summary>
public static class DHWebHelper {
    #region Http请求
    /// <summary>返回请求字符串和表单的名值字段，过滤空值和ViewState，同名时优先表单</summary>
    public static IDictionary<String, String> Params
    {
        get
        {
            var ctx = Pek.Webs.HttpContext.Current;
            if (ctx.Items["Params"] is IDictionary<String, String> dic) return dic;

            dic = GetParams(ctx, false, true, true, true, false);

            ctx.Items["Params"] = dic;

            return dic;
        }
    }

    /// <summary>获取请求参数字段，Key不区分大小写，合并多数据源</summary>
    /// <param name="ctx">Http上下文</param>
    /// <param name="route">从路由参数获取</param>
    /// <param name="query">从Url请求参数获取</param>
    /// <param name="form">从表单获取</param>
    /// <param name="body">从Body解析Json获取</param>
    /// <param name="mergeValue">同名是否合并参数值</param>
    /// <returns></returns>
    public static IDictionary<String, String> GetParams(this Microsoft.AspNetCore.Http.HttpContext ctx, Boolean route, Boolean query, Boolean form, Boolean body, Boolean mergeValue)
    {
        var req = ctx.Request;

        // 这里必须用可空字典，否则直接通过索引查不到数据时会抛出异常
        var dic = new NullableDictionary<String, String>(StringComparer.OrdinalIgnoreCase);

        // 依次从查询字符串、表单、body读取参数
        if (route)
        {
            foreach (var kv in req.RouteValues)
            {
                if (kv.Key.IsNullOrWhiteSpace()) continue;

                dic[kv.Key] = kv.Value?.ToString().Trim();
            }
        }

        if (query)
        {
            foreach (var kv in req.Query)
            {
                if (kv.Key.IsNullOrWhiteSpace()) continue;

                var v = kv.Value.ToString().Trim();
                if (mergeValue && dic.TryGetValue(kv.Key, out var old) && !old.IsNullOrEmpty())
                {
                    dic[kv.Key] = $"{old},{v}";
                }
                else
                {
                    dic[kv.Key] = v;
                }
            }
        }
        if (form && req.HasFormContentType)
        {
            foreach (var kv in req.Form)
            {
                if (kv.Key.IsNullOrWhiteSpace()) continue;
                if (kv.Key.StartsWithIgnoreCase("__VIEWSTATE")) continue;

                var v = kv.Value.ToString().Trim();
                if (mergeValue && dic.TryGetValue(kv.Key, out var old) && !old.IsNullOrEmpty())
                {
                    dic[kv.Key] = $"{old},{v}";
                }
                else
                {
                    dic[kv.Key] = v;
                }
            }
        }

        if (body)
        {
            // 尝试从body读取json格式的参数
            if (req.GetRequestBody<Object>() is NullableDictionary<String, Object> entityBody)
            {
                foreach (var kv in entityBody)
                {
                    var v = kv.Value?.ToString()?.Trim();
                    if (v.IsNullOrWhiteSpace()) continue;

                    if (mergeValue && dic.TryGetValue(kv.Key, out var old) && !old.IsNullOrEmpty())
                    {
                        dic[kv.Key] = $"{old},{v}";
                    }
                    else
                    {
                        dic[kv.Key] = v;
                    }
                }
            }
        }

        return dic;
    }

    /// <summary>获取原始请求Url，支持反向代理</summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static Uri GetRawUrl(this HttpRequest request)
    {
        // 加速，避免重复计算
        if (request.HttpContext.Items["_RawUrl"] is Uri uri) return uri;

        // 取请求头
        var url = request.GetEncodedUrl();
        uri = new Uri(url);

        uri = GetRawUrl(uri, k => request.Headers[k]);
        request.HttpContext.Items["_RawUrl"] = uri;

        return uri;
    }

    /// <summary>保存上传文件</summary>
    /// <param name="file">上传的文件</param>
    /// <param name="filename">保存的文件名</param>
    public static void SaveAs(this IFormFile file, String filename)
    {
        using var fs = new FileStream(filename, FileMode.OpenOrCreate);
        //file.OpenReadStream().CopyTo(fs);
        file.CopyTo(fs);
        fs.SetLength(fs.Position);
    }

    /// <summary>保存上传文件</summary>
    /// <param name="file">上传的文件</param>
    /// <param name="filename">保存的文件名</param>
    /// <param name="Length">要移除的字节长度</param>
    public static void SaveAs(this IFormFile file, String filename, Int32 Length)
    {
        using var fs = new FileStream(filename, FileMode.OpenOrCreate);
        // 将文件复制到文件流
        file.CopyTo(fs);
        // 获取文件现在的大小并减去4字节
        long updatedFileSize = fs.Length - Length;
        // 设置文件大小，这样的话会自动剪裁后面的指定字节
        fs.SetLength(updatedFileSize);
    }

    /// <summary>
    /// 保存指定长度的上传文件
    /// </summary>
    /// <param name="file">上传的文件</param>
    /// <param name="filename">保存的文件名</param>
    /// <param name="startPosition">起始位置</param>
    /// <param name="length">要保留的字节长度</param>
    public static void SaveAs(this IFormFile file, string filename, long startPosition, int length)
    {
        using var sourceStream = file.OpenReadStream();
        // 直接跳转到指定开始位置
        sourceStream.Seek(startPosition, SeekOrigin.Begin);
        // 准备一个相应大小的字节容器来存放我们要的数据
        byte[] buffer = new byte[length];
        // 开始读取
        sourceStream.Read(buffer, 0, length);
        // 创建目标文件，或者打开已经存在的文件用于数据写入
        using FileStream fs = new(filename, FileMode.OpenOrCreate);
        // 写入数据
        fs.Write(buffer, 0, buffer.Length);
    }

    private static Uri GetRawUrl(Uri uri, Func<String, String> headers)
    {
        var str = headers("HTTP_X_REQUEST_URI");
        if (str.IsNullOrEmpty()) str = headers("X-Request-Uri");

        if (str.IsNullOrEmpty())
        {
            // 阿里云CDN默认支持 X-Client-Scheme: https
            var scheme = headers("HTTP_X_CLIENT_SCHEME");
            if (scheme.IsNullOrEmpty()) scheme = headers("X-Client-Scheme");

            // nginx
            if (scheme.IsNullOrEmpty()) scheme = headers("HTTP_X_FORWARDED_PROTO");
            if (scheme.IsNullOrEmpty()) scheme = headers("X-Forwarded-Proto");

            if (!scheme.IsNullOrEmpty()) str = scheme + "://" + uri.ToString().Substring("://");
        }

        if (!str.IsNullOrEmpty()) uri = new Uri(uri, str);

        return uri;
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

        if (url.StartsWith("~/")) url = "/" + url[2..];

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

    #region 辅助

    public static Boolean ValidRobot(Microsoft.AspNetCore.Http.HttpContext ctx, UserAgentParser ua)
    {
        if (ua.Compatible.IsNullOrEmpty()) return true;

        // 判断爬虫
        var code = DHSetting.Current.RobotError;
        if (code > 0 && ua.IsRobot && !ua.Brower.IsNullOrEmpty())
        {
            var name = ua.Brower;
            var p = name.IndexOf('/');
            if (p > 0) name = name[..p];

            // 埋点
            using var span = DefaultTracer.Instance?.NewSpan($"bot:{name}", ua.UserAgent);

            ctx.Response.StatusCode = code;
            return false;
        }

        return true;
    }

    /// <summary>获取魔方设备Id。该Id代表一台设备，尽可能在多个应用中共用</summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    public static String FillDeviceId(Microsoft.AspNetCore.Http.HttpContext ctx)
    {
        // 准备Session，避免未启用Session时ctx.Session直接抛出异常
        var ss = ctx.Features.Get<ISessionFeature>()?.Session;
        if (ss != null && !ss.IsAvailable) ss = null;

        // http/https分开使用不同的Cookie名，避免站点同时支持http和https时，Cookie冲突
        var id = ss?.GetString("CubeDeviceId");
        if (id.IsNullOrEmpty()) id = ctx.Request.Cookies["CubeDeviceId"];
        if (id.IsNullOrEmpty()) id = ctx.Request.Cookies["CubeDeviceId0"];
        if (id.IsNullOrEmpty())
        {
            id = Rand.NextString(16);

            var option = new CookieOptions
            {
                HttpOnly = true,
                //Domain = domain,
                Expires = DateTimeOffset.Now.AddYears(10),
                SameSite = SameSiteMode.Unspecified,
                //Secure = true,
            };

            // https时，SameSite使用None，此时可以让cookie写入有最好的兼容性，跨域也可以读取
            if (ctx.Request.GetRawUrl().Scheme.EqualIgnoreCase("https"))
            {
                //var domain = CubeSetting.Current.CookieDomain;
                //if (!domain.IsNullOrEmpty())
                //{
                //    option.Domain = domain;
                //    option.SameSite = SameSiteMode.None;
                //    option.Secure = true;
                //}

                //option.HttpOnly = true;
                option.SameSite = SameSiteMode.None;
                option.Secure = true;

                ctx.Response.Cookies.Append("CubeDeviceId", id, option);
            }
            else
                ctx.Response.Cookies.Append("CubeDeviceId0", id, option);

            ss?.SetString("CubeDeviceId", id);
        }

        return id;
    }
    #endregion
}