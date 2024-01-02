﻿using NewLife;
using NewLife.Collections;
using NewLife.Model;
using NewLife.Security;

namespace XCode.Membership;

/// <summary>管理提供者接口</summary>
/// <remarks>
/// 管理提供者接口主要提供（或统一规范）用户提供者定位、用户查找登录等功能。
/// 只需要一个实现IManageUser接口的用户类即可实现IManageProvider接口。
/// IManageProvider足够精简，使得大多数用户可以自定义实现；
/// 也因为其简单稳定，大多数需要涉及用户与权限功能的操作，均可以直接使用该接口。
/// </remarks>
public interface IManageProvider : IServiceProvider
{
    /// <summary>当前登录用户，设为空则注销登录</summary>
    IManageUser Current { get; set; }

    /// <summary>当前租户。正在使用的租户</summary>
    ITenant Tenant { get; set; }

    /// <summary>密码提供者</summary>
    IPasswordProvider PasswordProvider { get; set; }

    /// <summary>获取当前用户</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    IManageUser GetCurrent(IServiceProvider context);

    /// <summary>设置当前用户</summary>
    /// <param name="user"></param>
    /// <param name="context"></param>
    void SetCurrent(IManageUser user, IServiceProvider context);

    ///// <summary>获取租户信息</summary>
    ///// <returns></returns>
    //Int32 GetTenant(IServiceProvider context);

    ///// <summary>设置租户</summary>
    ///// <param name="tenantId"></param>
    ///// <param name="context"></param>
    //void SetTenant(Int32 tenantId, IServiceProvider context);

    /// <summary>当前用户的所有租户</summary>
    /// <returns></returns>
    IList<ITenantUser> GetTenants();

    /// <summary>根据用户编号查找</summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    IManageUser FindByID(Object userid);

    /// <summary>根据用户帐号查找</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IManageUser FindByName(String name);

    /// <summary>登录</summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="rememberme">是否记住密码</param>
    /// <returns></returns>
    IManageUser Login(String username, String password, Boolean rememberme = false);

    /// <summary>注销</summary>
    void Logout();

    /// <summary>注册用户</summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <param name="roleid">角色</param>
    /// <param name="enable">是否启用</param>
    /// <returns></returns>
    IManageUser Register(String username, String password, Int32 roleid = 0, Boolean enable = false);

    /// <summary>修改密码</summary>
    /// <param name="username">用户名</param>
    /// <param name="newPassword">新密码</param>
    /// <param name="oldPassword">旧密码，如果未指定，则不校验</param>
    /// <returns></returns>
    IManageUser ChangePassword(String username, String newPassword, String oldPassword);

    /// <summary>当前用户（或租户）是否拥有指定菜单的某些权限</summary>
    /// <param name="menu"></param>
    /// <param name="flags"></param>
    /// <returns></returns>
    Boolean Has(IMenu menu, params PermissionFlags[] flags);

    /// <summary>获取服务</summary>
    /// <remarks>
    /// 其实IServiceProvider有该扩展方法，但是在FX2里面不方面使用，所以这里保留
    /// </remarks>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    TService GetService<TService>();
}

/// <summary>管理提供者</summary>
public abstract class ManageProvider : IManageProvider
{
    #region 静态实例
    /// <summary>当前管理提供者</summary>
    public static IManageProvider? Provider { get; set; }

    /// <summary>当前登录用户</summary>
    public static IUser? User
    {
        get => Provider?.Current as IUser;
        set
        {
            if (Provider != null && value is IManageUser user)
                Provider.Current = user;
        }
    }

    /// <summary>菜单工厂</summary>
    public static IMenuFactory? Menu => GetFactory<IMenu>() as IMenuFactory;

#if NET45
    private static readonly ThreadLocal<String> _UserHost = new();
#else
    private static readonly AsyncLocal<String> _UserHost = new();
#endif
    /// <summary>用户主机</summary>
    public static String UserHost { get => _UserHost.Value; set => _UserHost.Value = value; }
    #endregion

    #region IManageProvider 接口
    /// <summary>当前用户</summary>
    public virtual IManageUser Current { get => GetCurrent(); set => SetCurrent(value); }

    /// <summary>当前租户。正在使用的租户</summary>
    public ITenant Tenant { get; set; }

    /// <summary>当前用户的所有租户</summary>
    /// <returns></returns>
    public virtual IList<ITenantUser> GetTenants()
    {
        var user = Current;
        if (user == null || user.ID == 0) return null;

        var list = TenantUser.FindAllByUserId(user.ID);

        return list.Where(e => e.Enable).Cast<ITenantUser>().ToList();
    }

    /// <summary>密码提供者</summary>
    public IPasswordProvider PasswordProvider { get; set; } = new SaltPasswordProvider();

    /// <summary>获取当前用户</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public abstract IManageUser GetCurrent(IServiceProvider context = null);

    /// <summary>设置当前用户</summary>
    /// <param name="user"></param>
    /// <param name="context"></param>
    public abstract void SetCurrent(IManageUser user, IServiceProvider context = null);

    ///// <summary>获取当前用户租户</summary>
    ///// <returns></returns>
    //public abstract Int32 GetTenant(IServiceProvider context = null);

    ///// <summary>设置当前用户租户</summary>
    ///// <param name="tenantId"></param>
    ///// <param name="context"></param>
    //public abstract void SetTenant(Int32 tenantId, IServiceProvider context = null);

    /// <summary>根据用户编号查找</summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public virtual IManageUser FindByID(Object userid) => Membership.User.FindByID((userid + "").ToInt(-1));

    /// <summary>根据用户帐号查找</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public virtual IManageUser FindByName(String name) => Membership.User.FindForLogin(name);

    /// <summary>登录</summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="rememberme">是否记住密码</param>
    /// <returns></returns>
    public virtual IManageUser Login(String username, String password, Boolean rememberme) => Current = LoginCore(username, password);

    /// <summary>核心登录方法</summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public virtual IManageUser LoginCore(String username, String password)
    {
        try
        {
            if (String.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username), $"用户名或密码不正确！");

            // 过滤帐号中的空格，防止出现无操作无法登录的情况
            var account = username.Trim();
            // 登录时必须从数据库查找用户，缓存中的用户对象密码字段可能为空
            var user = Membership.User.FindForLogin(account);
            if (user == null) throw new EntityException("用户名或密码不正确！");
            if (!user.Enable) throw new EntityException("账号{0}被禁用！", account);

            var prv = PasswordProvider;
            if (prv == null) throw new ArgumentNullException(nameof(PasswordProvider));

            // 数据库为空密码，任何密码均可登录
            if (!user.Password.IsNullOrEmpty())
            {
                if (!prv.Verify(password, user.Password)) throw new EntityException("用户名或密码不正确！");

                // 旧式密码更新为新格式，更安全
                if (!user.Password.Contains("$"))
                    user.Password = prv.Hash(password);
            }
            else
            {
                user.Password = prv.Hash(password);
            }

            user.SaveLoginInfo();

            Membership.User.WriteLog("登录", true, $"用户[{user}]使用[{username}]登录成功");

            return user;
        }
        catch (Exception ex)
        {
            Membership.User.WriteLog("登录", false, username + "登录失败！" + ex.Message);
            throw;
        }
    }

    /// <summary>注销</summary>
    public virtual void Logout()
    {
        if (Current is IUser user) user.Logout();

        Current = null;
    }

    /// <summary>注册用户</summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <param name="roleid">角色</param>
    /// <param name="enable">是否启用。某些系统可能需要验证审核</param>
    /// <returns></returns>
    public virtual IManageUser Register(String username, String password, Int32 roleid, Boolean enable)
    {
        try
        {
            // 去重判断
            var user = Membership.User.FindForLogin(username);
            if (user != null) throw new ArgumentException(nameof(username), $"用户[{username}]已存在！");

            var pass = PasswordProvider.Hash(password);

            user = new User
            {
                Name = username,
                Password = pass,
                Enable = enable,
                RoleID = roleid
            };

            user.Register();

            Membership.User.WriteLog("注册", true, $"用户[{user}]使用[{username}]注册成功");

            return user;
        }
        catch (Exception ex)
        {
            Membership.User.WriteLog("注册", false, username + "注册失败！" + ex.Message);
            throw;
        }
    }

    /// <summary>修改密码</summary>
    /// <param name="username">用户名</param>
    /// <param name="newPassword">新密码</param>
    /// <param name="oldPassword">旧密码，如果未指定，则不校验</param>
    /// <returns></returns>
    public virtual IManageUser ChangePassword(String username, String newPassword, String oldPassword)
    {
        try
        {
            if (username.IsNullOrEmpty()) throw new ArgumentNullException(nameof(username), "该帐号不存在！");

            // 过滤帐号中的空格，防止出现误操作无法登录的情况
            var account = username.Trim();
            var user = Membership.User.Find(Membership.User.__.Name, account);
            if (user == null) throw new EntityException("帐号{0}不存在！", account);
            if (!user.Enable) throw new EntityException("账号{0}被禁用！", account);

            var prv = PasswordProvider;

            // 数据库为空密码，任何密码均可登录
            if (!oldPassword.IsNullOrEmpty())
            {
                if (!prv.Verify(oldPassword, user.Password)) throw new EntityException("用户名或密码不正确！");
            }

            user.Password = prv.Hash(newPassword);
            user.Update();

            Membership.User.WriteLog("修改密码", true, username);

            return user;
        }
        catch (Exception ex)
        {
            Membership.User.WriteLog("修改密码", false, username + "修改密码失败！" + ex.Message);
            throw;
        }
    }

    /// <summary>当前用户（或租户）是否拥有指定菜单的某些权限</summary>
    /// <param name="menu"></param>
    /// <param name="flags"></param>
    /// <returns></returns>
    public virtual Boolean Has(IMenu menu, params PermissionFlags[] flags)
    {
        // 优先判断租户
        var tenant = Tenant;
        if (tenant != null)
        {
            var tu = GetTenants().FirstOrDefault(e => e.TenantId == tenant.Id);
            if (tu != null) return tu.Has(menu, flags);
        }

        return (Current as IUser)?.Has(menu, flags) ?? false;
    }

    /// <summary>获取服务</summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public TService GetService<TService>() => (TService)GetService(typeof(TService));

    /// <summary>获取服务</summary>
    /// <param name="serviceType"></param>
    /// <returns></returns>
    public virtual Object GetService(Type serviceType) => null;
    #endregion

    #region 实体类扩展
    private static IDictionary<Type, IEntityFactory> _factories;
    private static void InitFactories()
    {
        if (_factories == null)
        {
            var fact = new NullableDictionary<Type, IEntityFactory>
            {
                [typeof(IRole)] = Role.Meta.Factory,
                [typeof(IMenu)] = Membership.Menu.Meta.Factory,
                [typeof(ILog)] = Log.Meta.Factory,
                [typeof(IUser)] = Membership.User.Meta.Factory
            };

            // 不想加锁，用原子操作
            //_factories = fact;
            Interlocked.CompareExchange(ref _factories, fact, null);
        }
    }

    private static void Register<TIEntity>(IEntityFactory factory)
    {
        InitFactories();

        _factories[typeof(TIEntity)] = factory;
    }

    /// <summary>根据实体类接口获取实体工厂</summary>
    /// <typeparam name="TIEntity"></typeparam>
    /// <returns></returns>
    internal static IEntityFactory GetFactory<TIEntity>()
    {
        InitFactories();

        return _factories[typeof(TIEntity)];
    }

    internal static T Get<T>() => (T)GetFactory<T>()?.Default;
    #endregion
}