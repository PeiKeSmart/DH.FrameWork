﻿using NewLife;
using NewLife.Log;

using Pek.Webs;

using HttpContext = Microsoft.AspNetCore.Http.HttpContext;

namespace DH.Services.WebMiddleware;

/// <summary>中间件助手</summary>
public static class MiddlewareHelper {
    /// <summary>检查是否强制跳转</summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    public static Boolean CheckForceRedirect(HttpContext ctx)
    {
        if (ctx.Request.Method != "GET") return false;

        var set = DHSetting.Current;
        if (set.ForceRedirect.IsNullOrEmpty()) return false;

        // 分解跳转地址
        var u = new MyUri(set.ForceRedirect);
        if (u.Host == "*") u.Host = null;

        if (u.Scheme.IsNullOrEmpty() && u.Host.IsNullOrEmpty() && u.Port == 0) return false;

        var uri = ctx.Request.GetRawUrl();
        if ((u.Scheme.IsNullOrEmpty() || uri.Scheme.EqualIgnoreCase(u.Scheme)) &&
            (u.Host.IsNullOrEmpty() || uri.Host.EqualIgnoreCase(u.Host)) &&
            (u.Port == 0 || u.Port == uri.Port)) return false;

        using var span = DefaultTracer.Instance?.NewSpan("ForceRedirect", uri + "");
        span?.AppendTag($"规则：{set.ForceRedirect}");

        // 重建url
        if (u.Scheme.IsNullOrEmpty()) u.Scheme = uri.Scheme;
        if (u.Host.IsNullOrEmpty()) u.Host = uri.Host;
        if (u.Port == 0) u.Port = uri.Port;

        var url = u.Scheme.EqualIgnoreCase("http", "ws") && u.Port == 80 ||
            u.Scheme.EqualIgnoreCase("https", "wss") && u.Port == 443 ?
            $"{u.Scheme}://{u.Host}{uri.PathAndQuery}" :
            $"{u.Scheme}://{u.Host}:{u.Port}{uri.PathAndQuery}";

        span?.AppendTag($"跳转：{url}");

        ctx.Response.Redirect(url);

        return true;
    }

    class MyUri {
        public String Scheme { get; set; }
        public String Host { get; set; }
        public Int32 Port { get; set; }
        public String PathAndQuery { get; set; }

        public MyUri(String value)
        {
            // 先处理头尾，再处理中间的主机和端口
            var p = value.IndexOf("://");
            if (p >= 0)
            {
                Scheme = value[..p];
                p += 3;
            }
            else
                p = 0;

            var p2 = value.IndexOf('/', p);
            if (p2 > 0)
            {
                PathAndQuery = value[p2..];
                value = value[p..p2];
            }
            else
                value = value[p..];

            // 拆分主机和端口，注意IPv6地址
            p2 = value.LastIndexOf(':');
            if (p2 > 0)
            {
                Host = value[..p2];
                Port = value[(p2 + 1)..].ToInt();
            }
            else
            {
                Host = value;
            }
        }
    }
}