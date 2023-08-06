using DH.AspNetCore.Webs;
using DH.Core.Webs;
using DH.Webs;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Net.Http.Headers;

using NewLife;
using NewLife.Common;
using NewLife.Log;
using NewLife.Model;

using System.Security.Principal;

using XCode;
using XCode.Membership;

using HttpContext = Microsoft.AspNetCore.Http.HttpContext;
using IServiceCollection = Microsoft.Extensions.DependencyInjection.IServiceCollection;
using JwtBuilder = NewLife.Web.JwtBuilder;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace DH.Services.Membership;

/// <summary>管理提供者助手</summary>
public static class ManagerProviderHelper {
    /// <summary>设置当前用户</summary>
    /// <param name="provider">提供者</param>
    /// <param name="context">Http上下文，兼容NetCore</param>
    public static void SetPrincipal(this IManageProvider provider, IServiceProvider context = null)
    {
        var ctx = ModelExtension.GetService<IHttpContextAccessor>(context)?.HttpContext;
        if (ctx == null) return;

        var user = provider.GetCurrent(context);
        if (user == null) return;

        if (user is not IIdentity id || ctx.User?.Identity == id) return;

        // 角色列表
        var roles = new List<String>();
        if (user is IUser user2) roles.AddRange(user2.Roles.Select(e => e + ""));

        var up = new GenericPrincipal(id, roles.ToArray());
        ctx.User = up;
        Thread.CurrentPrincipal = up;
    }

    /// <summary>尝试登录。如果Session未登录则借助Cookie</summary>
    /// <param name="provider">提供者</param>
    /// <param name="context">Http上下文，兼容NetCore</param>
    public static IManageUser TryLogin(this IManageProvider provider, HttpContext context)
    {
        var serviceProvider = context?.RequestServices;

        // 判断当前登录用户
        var user = provider.GetCurrent(serviceProvider);
        if (user == null)
        {
            // 尝试从Cookie登录
            user = provider.LoadCookie(true, context);
            if (user != null) provider.SetCurrent(user, serviceProvider);
        }

        // 如果Null直接返回
        if (user == null) return null;

        // 设置前端当前用户
        provider.SetPrincipal(serviceProvider);

        // 处理租户相关信息
        {
            var tlist = TenantUser.FindAllByUserId(user.ID);
            var tenantId = GetCookieTenantID(context);

            if (tlist.Any(e => e.TenantId == tenantId))
            {
                ChangeTenant(context, tenantId);
            }
            else
            {
                ChangeTenant(context, tlist.FirstOrDefault()?.TenantId ?? 0);
            }
        }

        return user;
    }

    /// <summary>生成令牌</summary>
    /// <returns></returns>
    public static JwtBuilder GetJwt()
    {
        var set = DHSetting.Current;

        // 生成令牌
        var ss = set.JwtOptions.Secret?.Split(':');
        if (ss == null || ss.Length < 2) throw new InvalidOperationException("未设置JWT算法和密钥");

        var jwt = new JwtBuilder
        {
            Algorithm = ss[0],
            Secret = ss[1],
        };

        return jwt;
    }

    #region Cookie
    /// <summary>从Cookie加载用户信息</summary>
    /// <param name="provider">提供者</param>
    /// <param name="autologin">是否自动登录</param>
    /// <param name="context">Http上下文，兼容NetCore</param>
    /// <returns></returns>
    public static IManageUser LoadCookie(this IManageProvider provider, Boolean autologin, HttpContext context)
    {
        var key = $"token-{SysConfig.Current.Name}";
        var req = context?.Request;
        var token = req?.Cookies[key];

        // 尝试从url中获取token
        if (token.IsNullOrEmpty() || token.Split(".").Length != 3) token = req?.Query["token"];
        if (token.IsNullOrEmpty() || token.Split(".").Length != 3) token = req?.Query["jwtToken"];

        // 尝试从头部获取token
        if (token.IsNullOrEmpty() || token.Split(".").Length != 3) token = req?.Headers[HeaderNames.Authorization];

        if (token.IsNullOrEmpty() || token.Split(".").Length != 3) return null;

        token = token.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

        var jwt = GetJwt();
        if (!jwt.TryDecode(token, out var msg))
        {
            XTrace.WriteLine("令牌无效：{0}, token={1}", msg, token);

            return null;
        }

        var user = jwt.Subject;
        if (user.IsNullOrEmpty()) return null;

        // 判断有效期
        if (jwt.Expire < DateTime.Now)
        {
            XTrace.WriteLine("令牌过期：{0} {1}", jwt.Expire, token);

            return null;
        }

        var u = provider.FindByName(user);
        if (u == null || !u.Enable) return null;

        // 保存登录信息。如果是json请求，不用记录自动登录
        if (autologin && u is IAuthUser mu && !req.IsAjaxRequest())
        {
            mu.SaveLogin(null);

            LogProvider.Provider.WriteLog("用户", "自动登录", true, $"{user} Time={jwt.IssuedAt} Expire={jwt.Expire} Token={token}", u.ID, u + "", ip: context.GetUserHost());
        }

        return u;
    }

    /// <summary>保存用户信息到Cookie</summary>
    /// <param name="provider">提供者</param>
    /// <param name="user">用户</param>
    /// <param name="expire">过期时间</param>
    /// <param name="context">Http上下文，兼容NetCore</param>
    public static void SaveCookie(this IManageProvider provider, IManageUser user, TimeSpan expire, HttpContext context)
    {
        var res = context?.Response;
        if (res == null) return;

        var set = DHSetting.Current;
        var option = new CookieOptions
        {
            SameSite = SameSiteMode.Unspecified,
        };
        // https时，SameSite使用None，此时可以让cookie写入有最好的兼容性，跨域也可以读取
        if (context.Request.GetRawUrl().Scheme.EqualIgnoreCase("https"))
        {
            //if (!set.CookieDomain.IsNullOrEmpty()) option.Domain = set.CookieDomain;
            option.SameSite = SameSiteMode.None;
            option.Secure = true;
        }

        var token = "";
        if (user != null)
        {
            // 令牌有效期，默认2小时
            var exp = DateTime.Now.Add(expire.TotalSeconds > 0 ? expire : TimeSpan.FromHours(2));
            var jwt = GetJwt();
            jwt.Subject = user.Name;
            jwt.Expire = exp;

            token = jwt.Encode(null);
            if (expire.TotalSeconds > 0) option.Expires = DateTimeOffset.Now.Add(expire);
        }
        else
        {
            option.Expires = DateTimeOffset.MinValue;
        }

        var key = $"token-{SysConfig.Current.Name}";
        res.Cookies.Append(key, token, option);

        context.Items["jwtToken"] = token;
    }
    #endregion

    /// <summary>改变选中的租户</summary>
    /// <param name="context"></param>
    /// <param name="tenantId">0管理员场景，大于0租户场景</param>
    public static void ChangeTenant(HttpContext context, Int32 tenantId)
    {
        var res = context?.Response;
        if (res == null) return;

        var set = DHSetting.Current;
        var option = new CookieOptions
        {
            SameSite = SameSiteMode.Unspecified,
        };
        // https时，SameSite使用None，此时可以让cookie写入有最好的兼容性，跨域也可以读取
        if (context.Request.GetRawUrl().Scheme.EqualIgnoreCase("https"))
        {
            //if (!set.CookieDomain.IsNullOrEmpty()) option.Domain = set.CookieDomain;
            option.SameSite = SameSiteMode.None;
            option.Secure = true;
        }

        if (tenantId < 0) option.Expires = DateTimeOffset.MinValue;

        res.Cookies.Append("TenantId", tenantId + "", option);
    }

    /// <summary>获取cookie中tenantId</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static Int32 GetCookieTenantID(HttpContext context)
    {
        var res = context?.Request;
        if (res == null) return 0;

        return res.Cookies["TenantId"].ToInt(0);
    }

    /// <summary>
    /// 添加管理提供者
    /// </summary>
    /// <param name="service"></param>
    public static void AddManageProvider(this IServiceCollection service)
    {
        service.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        service.TryAddSingleton<IManageProvider, ManageProvider2>();
    }

    /// <summary>
    /// 使用管理提供者
    /// </summary>
    /// <param name="app"></param>
    public static void UseManagerProvider(this IApplicationBuilder app)
    {
        XTrace.WriteLine("初始化ManageProvider");

        var provider = app.ApplicationServices;
        ManageProvider.Provider = ModelExtension.GetService<IManageProvider>(provider);
        //ManageProvider2.EndpointRoute = (IEndpointRouteBuilder)app.Properties["__EndpointRouteBuilder"];
        ManageProvider2.Context = ModelExtension.GetService<IHttpContextAccessor>(provider);

        // 初始化数据库
        //_ = Role.Meta.Count;
        EntityFactory.InitConnection("Membership");
        EntityFactory.InitConnection("Log");
        EntityFactory.InitConnection("DG");
    }
}