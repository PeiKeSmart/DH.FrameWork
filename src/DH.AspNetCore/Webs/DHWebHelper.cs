﻿using DH.AspNetCore.Webs;
using DH.Helpers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

using NewLife;
using NewLife.Collections;

using System.Text;
using System.Web;

namespace DH.Webs;

/// <summary>网页工具类</summary>
public static class DHWebHelper {
    #region Http请求
    /// <summary>返回请求字符串和表单的名值字段，过滤空值和ViewState，同名时优先表单</summary>
    public static IDictionary<String, String> Params
    {
        get
        {
            var ctx = HttpContext.Current;
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
        Uri uri = null;

        //// 配置
        //var ms = OAuthConfig.GetValids();
        //var mi = ms.FirstOrDefault(e => !e.AppUrl.IsNullOrEmpty());
        //if (mi != null) uri = new Uri(mi.AppUrl);

        // 取请求头
        if (uri == null)
        {
            var url = request.GetEncodedUrl();
            uri = new Uri(url);
        }

        return GetRawUrl(uri, k => request.Headers[k]);
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
        fs.Close();
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
        fs.Close();
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
}