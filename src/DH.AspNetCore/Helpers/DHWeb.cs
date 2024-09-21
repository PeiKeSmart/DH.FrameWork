using System.Net;
using System.Text;
using System.Web;

using DH.IO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

using Pek;

namespace DH.Helpers;

/// <summary>
/// Web操作
/// </summary>
public static partial class DHWeb
{
    #region Response(当前Http响应)

    /// <summary>
    /// 当前Http响应
    /// </summary>
    public static HttpResponse Response => Pek.Webs.HttpContext.Current?.Response;

    #endregion

    

    #region RequestType(请求类型)

    /// <summary>
    /// 请求类型
    /// </summary>
    public static string RequestType => Pek.Webs.HttpContext.Current?.Request?.Method;

    #endregion

    #region Form(表单)

    /// <summary>
    /// Form表单
    /// </summary>
    public static IFormCollection Form => Pek.Webs.HttpContext.Current?.Request?.Form;

    #endregion

    #region AccessToken(获取访问令牌)

    /// <summary>
    /// 获取访问令牌
    /// </summary>
    public static string AccessToken
    {
        get
        {
            var authorization = Pek.Helpers.DHWeb.Request?.Headers["Authorization"].SafeString();
            if (string.IsNullOrWhiteSpace(authorization))
                return null;
            var list = authorization.Split(' ');
            if (list.Length == 2)
                return list[1];
            return null;
        }
    }

    #endregion

    #region Body(请求正文)

    /// <summary>
    /// 请求正文
    /// </summary>
    public static string Body
    {
        get
        {
            Pek.Helpers.DHWeb.Request.EnableBuffering();
            return FileHelper.ToString(Pek.Helpers.DHWeb.Request.Body, isCloseStream: false);
        }
    }

    #endregion

    #region Url(请求地址)

    /// <summary>
    /// 获得请求的原始url(未转义)
    /// </summary>
    public static string Url => Pek.Helpers.DHWeb.Request?.GetDisplayUrl();

    /// <summary>
    /// 获得请求的原始url(转义)
    /// </summary>
    public static string EncodedUrl => Pek.Helpers.DHWeb.Request?.GetEncodedUrl();

    #endregion

    /// <summary>
    /// 引用地址
    /// </summary>
    public static string RefererUrl => Pek.Helpers.DHWeb.Request.Headers["Referer"].FirstOrDefault();

    #region Host(主机)

    /// <summary>
    /// 主机
    /// </summary>
    public static string Host => Pek.Webs.HttpContext.Current == null ? Dns.GetHostName() : GetClientHostName();

    /// <summary>
    /// 获取Web客户端主机名
    /// </summary>
    /// <returns></returns>
    private static string GetClientHostName()
    {
        var address = GetRemoteAddress();
        if (string.IsNullOrWhiteSpace(address))
        {
            return Dns.GetHostName();
        }
        var result = Dns.GetHostEntry(IPAddress.Parse(address)).HostName;
        if (result == "localhost.localdomain")
        {
            result = Dns.GetHostName();
        }
        return result;
    }

    /// <summary>
    /// 获取远程地址
    /// </summary>
    /// <returns></returns>
    private static string GetRemoteAddress()
    {
        return Pek.Webs.HttpContext.Current?.Request?.Headers["HTTP_X_FORWARDED_FOR"] ??
               Pek.Webs.HttpContext.Current?.Request?.Headers["REMOTE_ADDR"];
    }

    #endregion

    #region UserAgent(用户代理)

    /// <summary>
    /// 用户代理
    /// </summary>
    public static string UserAgent => Pek.Helpers.DHWeb.Request?.Headers["User-Agent"].SafeString();

    #endregion

    #region RootPath(根路径)

    /// <summary>
    /// 根路径
    /// </summary>
    public static string RootPath => Pek.Helpers.DHWeb.Environment?.ContentRootPath;

    #endregion

    #region WebRootPath(Web根路径)

    /// <summary>
    /// Web根路径，即wwwroot
    /// </summary>
    public static string WebRootPath => Pek.Helpers.DHWeb.Environment?.WebRootPath;

    #endregion

    #region ContentType(内容类型)

    /// <summary>
    /// 内容类型
    /// </summary>
    public static string ContentType => Pek.Webs.HttpContext.Current?.Request?.ContentType;

    #endregion

    #region QueryString(参数)

    /// <summary>
    /// 参数
    /// </summary>
    public static string QueryString => Pek.Webs.HttpContext.Current?.Request?.QueryString.ToString();

    #endregion

    #region IsLocal(是否本地请求)

    /// <summary>
    /// 是否本地请求
    /// </summary>
    public static bool IsLocal
    {
        get
        {
            var connection = Pek.Webs.HttpContext.Current?.Request?.HttpContext?.Connection;
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            if (connection.RemoteIpAddress.IsSet())
            {
                return connection.LocalIpAddress.IsSet()
                    ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                    : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            return true;
        }
    }

    /// <summary>
    /// 空IP地址
    /// </summary>
    private const string NullIpAddress = "::1";

    /// <summary>
    /// 是否已设置IP地址
    /// </summary>
    /// <param name="address">IP地址</param>
    private static bool IsSet(this IPAddress address) => address != null && address.ToString() != NullIpAddress;

    #endregion

    #region Client( Web客户端 )

    /// <summary>
    /// Web客户端，用于发送Http请求
    /// </summary>
    /// <returns></returns>
    public static DH.Webs.Clients.WebClient Client()
    {
        return new DH.Webs.Clients.WebClient();
    }

    /// <summary>
    /// Web客户端，用于发送Http请求
    /// </summary>
    /// <typeparam name="TResult">返回结果类型</typeparam>
    /// <returns></returns>
    public static DH.Webs.Clients.WebClient<TResult> Client<TResult>() where TResult : class
    {
        return new DH.Webs.Clients.WebClient<TResult>();
    }

    #endregion

    #region GetFiles(获取客户端文件集合)

    /// <summary>
    /// 获取客户端文件集合
    /// </summary>
    /// <returns></returns>
    public static List<IFormFile> GetFiles()
    {
        var result = new List<IFormFile>();
        var files = Pek.Webs.HttpContext.Current.Request.Form.Files;
        if (files == null || files.Count == 0)
        {
            return result;
        }

        result.AddRange(files.Where(file => file?.Length > 0));
        return result;
    }

    #endregion

    #region GetFile(获取客户端文件)

    /// <summary>
    /// 获取客户端文件
    /// </summary>
    /// <returns></returns>
    public static IFormFile GetFile()
    {
        var files = GetFiles();
        return files.Count == 0 ? null : files[0];
    }

    #endregion

    #region GetParam(获取请求参数)

    /// <summary>
    /// 获取请求参数，搜索路径：查询参数->表单参数->请求头
    /// </summary>
    /// <param name="name">参数名</param>
    public static string GetParam(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;
        if (Pek.Helpers.DHWeb.Request == null)
            return string.Empty;
        var result = string.Empty;
        if (Pek.Helpers.DHWeb.Request.Query != null)
            result = Pek.Helpers.DHWeb.Request.Query[name];
        if (string.IsNullOrWhiteSpace(result) == false)
            return result;
        if (Pek.Helpers.DHWeb.Request.Form != null)
            result = Pek.Helpers.DHWeb.Request.Form[name];
        if (string.IsNullOrWhiteSpace(result) == false)
            return result;
        if (Pek.Helpers.DHWeb.Request.Headers != null)
            result = Pek.Helpers.DHWeb.Request.Headers[name];
        return result;
    }

    #endregion

    #region UrlEncode(Url编码)

    /// <summary>
    /// Url编码
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="isUpper">编码字符是否转成大写，范例："http://"转成"http%3A%2F%2F"</param>
    /// <returns></returns>
    public static string UrlEncode(this string url, bool isUpper = false)
    {
        return UrlEncode(url, Encoding.UTF8, isUpper);
    }

    /// <summary>
    /// Url编码
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="encoding">字符编码</param>
    /// <param name="isUpper">编码字符是否转成大写，范例："http://"转成"http%3A%2F%2F"</param>
    /// <returns></returns>
    public static string UrlEncode(this string url, string encoding, bool isUpper = false)
    {
        encoding = string.IsNullOrWhiteSpace(encoding) ? "UTF-8" : encoding;
        return UrlEncode(url, Encoding.GetEncoding(encoding), isUpper);
    }

    /// <summary>
    /// Url编码
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="encoding">字符编码</param>
    /// <param name="isUpper">编码字符是否转成大写，范例："http://"转成"http%3A%2F%2F"</param>
    /// <returns></returns>
    public static string UrlEncode(this string url, Encoding encoding, bool isUpper = false)
    {
        var result = HttpUtility.UrlEncode(url, encoding);
        if (isUpper == false)
        {
            return result;
        }

        return GetUpperEncode(result);
    }

    /// <summary>
    /// 获取大写编码字符串
    /// </summary>
    /// <param name="encode">编码字符串</param>
    /// <returns></returns>
    private static string GetUpperEncode(string encode)
    {
        var result = new StringBuilder();
        int index = int.MinValue;
        for (int i = 0; i < encode.Length; i++)
        {
            string character = encode[i].ToString();
            if (character == "%")
            {
                index = i;
            }

            if (i - index == 1 || i - index == 2)
            {
                character = character.ToUpper();
            }

            result.Append(character);
        }

        return result.ToString();
    }

    #endregion

    #region UrlDecode(Url解码)

    /// <summary>
    /// Url解码
    /// </summary>
    /// <param name="url">url</param>
    /// <returns></returns>
    public static string UrlDecode(this string url)
    {
        return HttpUtility.UrlDecode(url);
    }

    /// <summary>
    /// Url解码
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="encoding">字符编码</param>
    /// <returns></returns>
    public static string UrlDecode(this string url, Encoding encoding)
    {
        return HttpUtility.UrlDecode(url, encoding);
    }

    #endregion

    #region Redirect(跳转到指定链接)

    /// <summary>
    /// 跳转到指定链接
    /// </summary>
    /// <param name="url">链接</param>
    public static void Redirect(string url) => Response?.Redirect(url);

    #endregion

    #region Write(输出内容)

    /// <summary>
    /// 输出内容
    /// </summary>
    /// <param name="text">内容</param>
    public static void Write(string text)
    {
        Response.ContentType = "text/plain;charset=utf-8";
        Task.Run(async () => { await Response.WriteAsync(text); }).GetAwaiter().GetResult();
    }

    #endregion

    #region Write(输出文件)

    /// <summary>
    /// 输出文件
    /// </summary>
    /// <param name="stream">文件流</param>
    public static void Write(FileStream stream)
    {
        long size = stream.Length;
        byte[] buffer = new byte[size];
        stream.Read(buffer, 0, (int)size);
        stream.Dispose();
        File.Delete(stream.Name);

        Response.ContentType = "application/octet-stream";
        Response.Headers.Add("Content-Disposition", "attachment;filename=" + WebUtility.UrlEncode(Path.GetFileName(stream.Name)));
        Response.Headers.Add("Content-Length", size.ToString());

        Task.Run(async () => { await Response.Body.WriteAsync(buffer, 0, (int)size); }).GetAwaiter().GetResult();
        Response.Body.Close();
    }

    #endregion

    #region GetBodyAsync(获取请求正文)

    /// <summary>
    /// 获取请求正文
    /// </summary>
    /// <returns></returns>
    public static async Task<string> GetBodyAsync()
    {
        Pek.Helpers.DHWeb.Request.EnableBuffering();
        return await FileHelper.ToStringAsync(Pek.Helpers.DHWeb.Request.Body, isCloseStream: false);
    }

    #endregion

    /// <summary>
    /// 返回绝对地址
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string AbsoluteUri(this HttpRequest request)
    {
        var absoluteUri = string.Concat(
                      request.Scheme,
                      "://",
                      request.Host.ToUriComponent(),
                      request.PathBase.ToUriComponent(),
                      request.Path.ToUriComponent(),
                      request.QueryString.ToUriComponent());

        return absoluteUri;
    }

    /// <summary>
    /// 获取当前站点Url
    /// </summary>
    /// <returns></returns>
    public static string GetSiteUrl()
    {
        return Pek.Helpers.DHWeb.Request.Scheme + "://" + Pek.Helpers.DHWeb.Request.Host;
    }

    public enum AgentType
    {
        Android = 0,
        IPhone = 1,
        IPad = 2,
        WindowsPhone = 3,
        Windows = 4,
        Wechat = 6,
        MacOS = 7
    }

    /// <summary>
    /// 获取客户端信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static AgentType UserAgentType(this HttpRequest request)
    {
        var userAgent = request.Headers["User-Agent"].ToString();
        switch (userAgent)
        {
            case string android when android.Contains("MicroMessenger"):
                return AgentType.Wechat;
            case string android when android.Contains("Android"):
                return AgentType.Android;
            case string android when android.Contains("iPhone"):
                return AgentType.IPhone;
            case string android when android.Contains("iPad"):
                return AgentType.IPad;
            case string android when android.Contains("Windows Phone"):
                return AgentType.WindowsPhone;
            case string android when android.Contains("Windows NT"):
                return AgentType.Windows;
            case string android when android.Contains("Mac OS"):
                return AgentType.MacOS;
        }
        return AgentType.Android;
    }

    #region DownloadAsync(下载)

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="fileName">文件名。包含扩展名</param>
    public static async Task DownloadFileAsync(string filePath, string fileName)
    {
        await DownloadFileAsync(filePath, fileName, Encoding.UTF8);
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="fileName">文件名。包含扩展名</param>
    /// <param name="encoding">字符编码</param>
    public static async Task DownloadFileAsync(string filePath, string fileName, Encoding encoding)
    {
        var bytes = FileHelper.ReadToBytes(filePath);
        await DownloadAsync(bytes, fileName, encoding);
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="fileName">文件名。包含扩展名</param>
    public static async Task DownloadAsync(Stream stream, string fileName)
    {
        await DownloadAsync(stream, fileName, Encoding.UTF8);
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="fileName">文件名。包含扩展名</param>
    /// <param name="encoding">字符编码</param>
    public static async Task DownloadAsync(Stream stream, string fileName, Encoding encoding)
    {
        await DownloadAsync(await FileHelper.ToBytesAsync(stream), fileName, encoding);
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="bytes">字节流</param>
    /// <param name="fileName">文件名。包含扩展名</param>
    public static async Task DownloadAsync(byte[] bytes, string fileName)
    {
        await DownloadAsync(bytes, fileName, Encoding.UTF8);
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="bytes">字节流</param>
    /// <param name="fileName">文件名。包含扩展名</param>
    /// <param name="encoding">字符编码</param>
    /// <returns></returns>
    public static async Task DownloadAsync(byte[] bytes, string fileName, Encoding encoding)
    {
        if (bytes == null || bytes.Length == 0)
        {
            return;
        }

        fileName = fileName.Replace(" ", "");
        fileName = UrlEncode(fileName, encoding);
        Response.ContentType = "application/octet-stream";
        Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");
        Response.Headers.Add("Content-Length", bytes.Length.ToString());
        await Response.Body.WriteAsync(bytes, 0, bytes.Length);
    }

    #endregion

}