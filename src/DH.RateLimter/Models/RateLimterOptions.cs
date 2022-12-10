using System;
using DH.RateLimter.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DH.RateLimter
{
    /// <summary>
    /// 配置项
    /// </summary>
    public class RateLimterOptions
    {
        /// <summary>
        /// 取得认证用户身份
        /// </summary>
        public Func<HttpContext, string> OnUserIdentity = context => context.GetDefaultUserIdentity();

        /// <summary>
        /// 取得客户端IP地址
        /// </summary>
        public Func<HttpContext, string> OnIpAddress = context => context.GetIpAddress();

        public Func<HttpContext, Valve, IntercepteWhere, IActionResult> onIntercepted = (context, valve, where) => { return new RateLimterResult { Content = "访问过于频繁，请稍后重试！" }; };
    }
}
