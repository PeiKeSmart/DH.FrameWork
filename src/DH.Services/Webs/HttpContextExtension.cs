using DH.Services.Common;

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace DH.Core.Webs;

public static class HttpContextExtension
{
    /// <summary>
    /// 地理位置信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string Location(this HttpRequest request)
    {
        return (string)request.HttpContext.Items.GetOrAdd("ip.location", () =>
        {
            var (location, network) = request.HttpContext.Connection.RemoteIpAddress.DHGetIPLocation();
            return location + "|" + network;
        });
    }

    /// <summary>
    /// 是否是机器人访问
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    public static bool IsRobot(this HttpRequest req) => UserAgent.Parse(req.Headers[HeaderNames.UserAgent].ToString()).IsRobot;
}