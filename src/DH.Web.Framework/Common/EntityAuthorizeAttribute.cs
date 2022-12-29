using DH.Core.Infrastructure;
using DH.Entity;
using DH.Web.Framework.Admin;
using DH.Web.Framework.Membership;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using NewLife;
using NewLife.Log;
using NewLife.Reflection;

using System.Collections.Concurrent;
using System.Reflection;

using XCode;
using XCode.Membership;

namespace DH.Web.Framework.Common;

/// <summary>实体授权特性</summary>
public class EntityAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    #region 属性
    /// <summary>授权项</summary>
    public PermissionFlags Permission { get; }
    #endregion

    #region 构造
    static EntityAuthorizeAttribute() => XTrace.WriteLine("注册过滤器：{0}", typeof(EntityAuthorizeAttribute).FullName);

    /// <summary>实例化实体授权特性</summary>
    public EntityAuthorizeAttribute() { }

    /// <summary>实例化实体授权特性</summary>
    /// <param name="permission"></param>
    public EntityAuthorizeAttribute(PermissionFlags permission)
    {
        if (permission <= PermissionFlags.None) throw new ArgumentNullException(nameof(permission));

        Permission = permission;
    }
    #endregion

    #region 方法
    /// <summary>授权发生时触发</summary>
    /// <param name="filterContext"></param>
    public void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        /*
         * 验证范围：
         * 1，管理区域下的所有控制器
         * 2，所有带有EntityAuthorize特性的控制器或动作
         */
        var act = filterContext.ActionDescriptor;
        var ctrl = (ControllerActionDescriptor)act;

        // 允许匿名访问时，直接跳过检查
        if (
            ctrl.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute)) ||
            ctrl.ControllerTypeInfo.IsDefined(typeof(AllowAnonymousAttribute))) return;

        // 如果控制器或者Action放有该特性，则跳过全局
        var hasAtt =
            ctrl.MethodInfo.IsDefined(typeof(EntityAuthorizeAttribute), true) ||
            ctrl.ControllerTypeInfo.IsDefined(typeof(EntityAuthorizeAttribute));

        // 只验证管辖范围
        var create = false;
        //if (!AreaBase.Contains(ctrl))
        //{
        //    if (!hasAtt) return;

        //    // 不属于后台管理而又加了权限特性，需要创建菜单
        //    create = true;
        //}

        // 根据控制器定位资源菜单
        var menu = ResolveMenu(filterContext, create);

        // 如果已经处理过，就不处理了
        if (filterContext.Result != null) return;

        if (!AuthorizeCore(filterContext.HttpContext, menu))
        {
            HandleUnauthorizedRequest(filterContext, menu);
        }
    }

    /// <summary>授权核心</summary>
    /// <param name="httpContext"></param>
    /// <param name="menu"></param>
    /// <returns></returns>
    private Boolean AuthorizeCore(Microsoft.AspNetCore.Http.HttpContext httpContext, IMenu menu)
    {
        var prv = ManageProvider.Provider;
        var ctx = httpContext;

        // 判断当前登录用户
        var user = ManagerProviderHelper.TryLogin(prv, httpContext);
        if (user == null) return false;

        // 判断权限
        if (menu != null)
        {
            if (user is IUser user2) return user2.Has(menu, Permission);

            var msg = $"访问菜单 {menu} 需要 {Permission.GetDescription()} 权限";
            LogProvider.Provider.WriteLog("访问", "拒绝", false, msg, ip: ctx.GetUserHost());
        }
        else
        {
            LogProvider.Provider.WriteLog("访问", "拒绝", false, "无法找到菜单", ip: ctx.GetUserHost());
        }

        return false;
    }

    /// <summary>未认证请求</summary>
    /// <param name="filterContext"></param>
    /// <param name="menu"></param>
    private void HandleUnauthorizedRequest(AuthorizationFilterContext filterContext, IMenu menu)
    {
        // 来到这里，有可能没登录，有可能没权限
        var prv = ManageProvider.Provider;
        if (prv?.Current == null)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.Result = new JsonResult(new { code = 401, msg = LocaleStringResource.GetResource("没有登录或登录超时！") });
            }
            else
            {
                var retUrl = filterContext.HttpContext.Request.GetEncodedPathAndQuery();
                //.Host.ToString();//.Url?.PathAndQuery;

                var _cookie = EngineContext.Current.Resolve<ICookie>();
                var Sid = _cookie.GetValue<Int64>(DHSetting.Current.SidName);
                if (Sid <= 0)
                {
                    // 生成Sid
                    Sid = Id.GetSId();
                    _cookie.SetValue(DHSetting.Current.SidName, Sid);
                }

                var rurl = $"~/{AdminArea.AreaName}/Login".AppendReturn(retUrl).ToLower();
                filterContext.Result = new RedirectResult(rurl);
            }
        }
        else
        {
            filterContext.Result = NoPermission(filterContext, menu, Permission);
        }
    }

    /// <summary>无权访问</summary>
    /// <param name="filterContext"></param>
    /// <param name="permission"></param>
    /// <param name="menu"></param>
    /// <returns></returns>
    public static ActionResult NoPermission(AuthorizationFilterContext filterContext, IMenu menu, PermissionFlags permission)
    {
        var act = (ControllerActionDescriptor)filterContext.ActionDescriptor;
        var ctrl = act;

        var ctx = filterContext.HttpContext;

        var res = $"[{ctrl.ControllerName}/{act.ActionName}]";
        var msg = $"访问资源 {res} 需要 {permission.GetDescription()} 权限";
        LogProvider.Provider.WriteLog("访问", "拒绝", false, msg, ip: ctx.GetUserHost());

        if (filterContext.HttpContext.Request.IsAjaxRequest())
        {
            filterContext.HttpContext.Response.StatusCode = 403;
            return new JsonResult(new { code = 403, msg = msg });
        }

        var vr = new ViewResult()
        {
            ViewName = "NoPermission"
        };

        vr.ViewData =
            new ViewDataDictionary(new EmptyModelMetadataProvider(), filterContext.ModelState)
            {
                ["Resource"] = res,
                ["Permission"] = permission,
                ["Menu"] = menu
            };
        return vr;
    }

    private IMenu ResolveMenu(AuthorizationFilterContext filterContext, Boolean create)
    {
        var act = (ControllerActionDescriptor)filterContext.ActionDescriptor;
        //var ctrl = act.ControllerDescriptor;
        var type = act.ControllerTypeInfo;
        var fullName = type.FullName + "." + act.ActionName;
        var url = filterContext.HttpContext.Request.Path;

        var ctx = filterContext.HttpContext;
        var mf = ManageProvider.Menu;
        var menu = ctx.Items["CurrentMenu"] as IMenu;
        if (menu == null)
        {
            menu = mf.FindByFullName(fullName) ?? mf.FindByFullName(type.FullName) ?? mf.FindByUrl(url) ?? mf.FindByUrl("~" + url);

            // 兼容旧版本视图权限
            ctx.Items["CurrentMenu"] = menu;
        }

        // 创建菜单
        if (create)
        {
            if (CreateMenu(type)) menu = mf.FindByFullName(fullName);
            //var name = type.Namespace.TrimEnd(".Controllers");
            //var root = mf.FindByFullName(name);
            //if (root == null)
            //{
            //    root = mf.Root.GetType().CreateInstance() as IMenu;
            //    root.FullName = name;
            //    root.Name = name;
            //    (root as IEntity).Insert();
            //}

            //var node = mf.Root.GetType().CreateInstance() as IMenu;
            //node.FullName = type.FullName + "." + act.ActionName;
            //node.Name = type.Name;
            //node.DisplayName = type.GetDisplayName();
            //node.ParentID = root.ID;
            //(node as IEntity).Insert();
        }

        if (menu == null) XTrace.WriteLine("设计错误！验证权限时无法找到[{0}/{1}]的菜单", type.FullName, act.ActionName);

        return menu;
    }

    private static readonly ConcurrentDictionary<String, Type> _ss = new ConcurrentDictionary<String, Type>();
    private Boolean CreateMenu(Type type)
    {
        if (!_ss.TryAdd(type.Namespace, type)) return false;

        using var span = DefaultTracer.Instance?.NewSpan(nameof(CreateMenu), type.FullName);

        var mf = ManageProvider.Menu;
        var ms = mf.ScanController(type.Namespace.TrimEnd(".Controllers"), type.Assembly, type.Namespace);

        var root = mf.FindByFullName(type.Namespace);
        if (root != null)
        {
            root.Url = "~";
            (root as IEntity).Update();
        }

        // 遍历菜单，设置权限项
        foreach (var controller in ms)
        {
            if (controller.FullName.IsNullOrEmpty()) continue;

            var ctype = type.Assembly.GetType(controller.FullName);
            //ctype = controller.FullName.GetTypeEx(false);
            if (ctype == null) continue;

            // 添加该类型下的所有Action
            foreach (var method in ctype.GetMethods())
            {
                if (method.IsStatic || !method.IsPublic || method.IsVirtual) continue;
                if (!method.ReturnType.As<IActionResult>() && !method.ReturnType.As<Task<IActionResult>>()) continue;
                if (method.GetCustomAttribute<AllowAnonymousAttribute>() != null) continue;

                var att = method.GetCustomAttribute<EntityAuthorizeAttribute>();
                if (att != null && att.Permission > PermissionFlags.None)
                {
                    var dn = method.GetDisplayName();
                    var pmName = !dn.IsNullOrEmpty() ? dn : method.Name;
                    if (att.Permission <= PermissionFlags.Delete) pmName = att.Permission.GetDescription();
                    controller.Permissions[(Int32)att.Permission] = pmName;
                }
            }

            controller.Url = "~/" + ctype.Name.TrimEnd("Controller");

            (controller as IEntity).Update();
        }

        return true;
    }
    #endregion
}