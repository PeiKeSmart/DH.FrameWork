﻿using DH.Entity;
using DH.Services.Webs;
using DH.Web.Framework.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using NewLife;
using NewLife.Collections;
using NewLife.Log;
using NewLife.Model;
using NewLife.Serialization;

using XCode;
using XCode.Membership;

namespace DH.Services.Membership;

/// <inheritdoc />
public class ManageProvider2 : ManageProvider {
    #region 静态实例
    internal static IHttpContextAccessor Context;

    /// <summary>
    /// 节点路由
    /// </summary>
    public static IEndpointRouteBuilder EndpointRoute { get; set; }

    static ManageProvider2()
    {
        //// 此处实例化会触发父类静态构造函数
        //var ioc = ObjectContainer.Current;
        //ioc.Register<IManageProvider, ManageProvider2>()
        //.Register<IManageUser, User>();

        Register<IRole>(Role.Meta.Factory);
        Register<IMenu>(XCode.Membership.Menu.Meta.Factory);
        Register<XCode.Membership.ILog>(Log.Meta.Factory);
        Register<IUser>(UserE.Meta.Factory);
    }
    #endregion

    #region 属性
    /// <summary>保存于Session的凭证</summary>
    public String SessionKey { get; set; } = "Admin";
    #endregion

    #region IManageProvider 接口
    /// <summary>获取当前用户。先找Items，再找Session2，没有自动登录能力</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override IManageUser GetCurrent(IServiceProvider context = null)
    {
        var ctx = (ModelExtension.GetService<IHttpContextAccessor>(context) ?? Context)?.HttpContext;
        if (ctx == null) return null;

        try
        {
            if (ctx.Items["CurrentUser"] is IManageUser user) return user;

            var session = ctx.Items["Session"] as IDictionary<String, Object>;

            user = session?[SessionKey] as IManageUser;
            ctx.Items["CurrentUser"] = user;

            return user;
        }
        catch (InvalidOperationException ex)
        {
            // 这里捕获一下，防止初始化应用中session还没初始化好报的异常
            // 这里有个问题就是这里的ctx会有两个不同的值
            XTrace.WriteException(ex);
            return null;
        }
    }

    /// <summary>设置当前用户。仅修改Items和Session2，不干涉Cookie</summary>
    /// <param name="user"></param>
    /// <param name="context"></param>
    public override void SetCurrent(IManageUser user, IServiceProvider context = null)
    {
        using var span = DefaultTracer.Instance?.NewSpan(nameof(SetCurrent), user);

        var ctx = (ModelExtension.GetService<IHttpContextAccessor>(context) ?? Context)
            ?.HttpContext;
        if (ctx == null) return;

        ctx.Items["CurrentUser"] = user;

        var session = ctx.Items["Session"] as IDictionary<String, Object>;
        if (session == null) return;

        var key = SessionKey;
        // 特殊处理注销
        if (user == null)
        {
            session.Remove(key);
            session.Remove("userId");
        }
        else
        {
            session[key] = user;
            session["userId"] = user.ID;
        }
    }

    /// <summary>登录</summary>
    /// <param name="name"></param>
    /// <param name="password"></param>
    /// <param name="remember">是否记住密码</param>
    /// <returns></returns>
    public override IManageUser Login(String name, String password, Boolean remember)
    {
        IManageUser user = null;

        // OAuth密码模式登录
        var oauths = OAuthConfig.GetValids(GrantTypes.Password);
        if (oauths.Count > 0)
            user = LoginByOAuth(oauths[0], name, password);
        else
            user = base.Login(name, password, remember);

        user = CheckAgent(user) as User;
        Current = user;

        // 过期时间
        var set = DHSetting.Current;
        var expire = TimeSpan.FromMinutes(0);
        if (remember && user != null)
        {
            expire = TimeSpan.FromDays(365);
        }
        else
        {
            if (set.SessionTimeout > 0)
                expire = TimeSpan.FromSeconds(set.SessionTimeout);
        }

        // 保存Cookie
        var context = Context?.HttpContext;
        this.SaveCookie(user, expire, context);

        return user;
    }

    private SsoClient _client;
    private IManageUser LoginByOAuth(OAuthConfig oa, String username, String password)
    {
        _client ??= new SsoClient
        {
            Server = oa.Server,
            AppId = oa.AppId,
            Secret = oa.Secret,
            SecurityKey = oa.SecurityKey,
        };

        //var ti = _client.GetToken(username, password).Result;
        //var ui = _client.GetUser(ti.AccessToken).Result as User;
        var ui = _client.UserAuth(username, password).Result;

        var set = DHSetting.Current;
        var log = LogProvider.Provider;

        // 仅验证登录，不要角色信息
        if (FindByName(username) is not User user)
        {
            if (!set.AutoRegister && !oa.AutoRegister)
            {
                log.WriteLog(typeof(User), "SSO登录", false, $"无法找到[{username}]，且没有打开自动注册", 0, username);

                throw new XException($"无法找到[{username}]，且没有打开自动注册");
            }

            user = new User
            {
                Code = ui["usercode"] as String,
                Name = username,
                DisplayName = ui["nickname"] as String,

                Enable = true,
                RegisterTime = DateTime.Now,
            };

            // 新注册用户采用框架默认角色
            var defRole = oa.AutoRole;
            if (defRole.IsNullOrEmpty()) defRole = set.DefaultRole;
            user.RoleID = Role.GetOrAdd(defRole).ID;
        }

        user.Logins++;
        user.LastLogin = DateTime.Now;
        user.Save();

        log.WriteLog(user.GetType(), "OAuth登录", true, $"用户[{user}]使用[{username}]登录[{_client.AppId}]成功！" + Environment.NewLine + ui.ToJson());

        return user;
    }

    /// <summary>检查委托代理</summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public IManageUser CheckAgent(IManageUser user)
    {
        if (user == null) return user;

        // 查找该用户是否有可用待立项，按照创建代理的先后顺序
        var list = PrincipalAgent.GetAllValidByAgentId(user.ID);
        if (list.Count == 0) return user;

        // 脏数据检查
        foreach (var item in list)
        {
            // 没有次数或者已过期，则禁用
            if (item.Enable && (item.Times == 0 || item.Expire.Year > 2000 && item.Expire < DateTime.Now))
            {
                item.Enable = false;
                item.Update();
            }
        }

        // 查找一个可用项
        var pa = list.FirstOrDefault(e => e.Enable);
        if (pa == null || pa.Principal == null) return user;

        var roles = pa.Principal?.Roles;
        if (roles != null && roles.Any(e => e.IsSystem))
        {
            pa.Enable = false;
            pa.Remark = "安全起见，不得代理系统管理员";
            pa.Update();

            LogProvider.Provider.WriteLog("用户", "代理", false, $"安全起见，[{pa.AgentName}]不得代理系统管理员[{pa.PrincipalName}]的身份权限", pa.AgentId, pa.AgentName);

            return user;
        }

        pa.Times--;
        if (pa.Times == 0) pa.Enable = false;
        pa.Update();

        LogProvider.Provider.WriteLog("用户", "委托", true, $"委托[{pa.AgentName}]使用[{pa.PrincipalName}]的身份权限", pa.PrincipalId, pa.PrincipalName);
        LogProvider.Provider.WriteLog("用户", "代理", true, $"[{pa.AgentName}]代理使用[{pa.PrincipalName}]的身份权限", pa.AgentId, pa.AgentName);

        return pa.Principal as IManageUser;
    }

    /// <summary>注销</summary>
    public override void Logout()
    {
        if (Current is User user) UserService.ClearOnline(user);

        // 注销时销毁所有Session
        var context = Context?.HttpContext;
        var session = context.Items["Session"] as IDictionary<String, Object>;
        session?.Clear();

        // 销毁Cookie
        this.SaveCookie(null, TimeSpan.Zero, context);

        base.Logout();
    }
    #endregion

    #region 实体类扩展
    private static IDictionary<Type, IEntityFactory> _factories = new NullableDictionary<Type, IEntityFactory>();

    private static void Register<TIEntity>(IEntityFactory factory) => _factories[typeof(TIEntity)] = factory;

    /// <summary>根据实体类接口获取实体工厂</summary>
    /// <typeparam name="TIEntity"></typeparam>
    /// <returns></returns>
    public static IEntityFactory GetFactory<TIEntity>() => _factories[typeof(TIEntity)];

    public static T Get<T>() => (T)GetFactory<T>()?.Default;
    #endregion
}