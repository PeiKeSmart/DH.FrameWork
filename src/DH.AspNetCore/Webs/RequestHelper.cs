﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

using NewLife;
using NewLife.Serialization;

namespace DH.AspNetCore.Webs;

/// <summary>请求助手类</summary>
public static class RequestHelper {
    /// <summary>
    /// 从请求中获取值
    /// </summary>
    /// <param name="request"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static String GetRequestValue(this HttpRequest request, String key)
    {
        var value = new StringValues();

        if (request.HasFormContentType) value = request.Form[key];

        if (value.Count > 0) return value;

        value = request.Query[key];

        if (value.Count > 0) return value.ToString();

        // 拒绝output关键字，避免死循环
        if (key == "output") return null;

        return null;
    }

    /// <summary>
    /// 确定指定的HTTP请求是否是Ajax请求
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static Boolean IsAjaxRequest(this HttpRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        if (request.Headers != null && request.Headers["X-Requested-With"] == "XMLHttpRequest") return true;
        if (request.ContentType.EqualIgnoreCase("application/json")) return true;

        if (request.Headers["Accept"].Any(e => e.Split(',').Any(a => a.Trim() == "application/json"))) return true;

        if (request.GetRequestValue("output").EqualIgnoreCase("json")) return true;

        return false;
    }

    /// <summary>
    /// 获取请求中的body对象，第一次解析后存储在HttpContext.Items["RequestBody"]中
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>如果类型是Object，返回的类型则是<see cref="NullableDictionary{String,Object}"/></remarks>
    public static T GetRequestBody<T>(this HttpRequest request) where T : class, new() => GetRequestBody(request, typeof(T)) as T;

    /// <summary>
    /// 获取请求主体部分
    /// </summary>
    /// <param name="request"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Object GetRequestBody(this HttpRequest request, Type type)
    {
        try
        {
            if (!request.IsAjaxRequest()) return null;

            var requestBody = request.HttpContext.Items["RequestBody"];
            if (requestBody != null) return requestBody;

            //// 允许同步IO
            //var ft = request.HttpContext.Features.Get<Microsoft.AspNetCore.Http.Features.IHttpBodyControlFeature>();
            //if (ft != null) ft.AllowSynchronousIO = true;

            //var body = request.Body.ToStr();

            request.EnableBuffering();
            request.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(request.Body);
            var body = reader.ReadToEndAsync().GetAwaiter().GetResult();
            request.Body.Seek(0, SeekOrigin.Begin);

            var entityBody = body.ToJsonEntity(type);
            request.HttpContext.Items["RequestBody"] = entityBody;

            return entityBody;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}