using DH.Core.Events;
using DH.Core.Infrastructure;
using DH.Core.Model.EventModel;
using DH.Entity;
using DH.Model;
using DH.Web.Framework.Admin;
using DH.Web.Framework.Membership;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

using NewLife;
using NewLife.Log;
using NewLife.Reflection;

using System.Collections.Concurrent;
using System.Reflection;

using XCode.Membership;

namespace DH.Web.Framework.Common;

/// <summary>区域特性基类</summary>
/// <remarks>
/// 提供以下功能：
/// 1，区域名称。从类名中截取。其中DisplayName特性作为菜单中文名。
/// 2，静态构造注册一次视图引擎、绑定提供者、过滤器
/// 3，注册区域默认路由
/// </remarks>
public class AreaBase : AreaAttribute
{
    private static ConcurrentDictionary<Type, Type> _areas = new ConcurrentDictionary<Type, Type>();

    /// <summary>实例化区域注册</summary>
    public AreaBase(String areaName) : base(areaName) => RegisterArea(GetType());

    /// <summary>注册区域，每个继承此区域特性的类的静态构造函数都调用此方法，以进行相关注册</summary>
    public static void RegisterArea<T>() where T : AreaBase => RegisterArea(typeof(T));

    /// <summary>注册区域，每个继承此区域特性的类的静态构造函数都调用此方法，以进行相关注册</summary>
    public static void RegisterArea<T>(IApplicationBuilder app) where T : AreaBase => RegisterArea(typeof(T), app);

    /// <summary>注册区域，每个继承此区域特性的类的静态构造函数都调用此方法，以进行相关注册</summary>
    public static void RegisterArea(Type areaType, IApplicationBuilder app = null)
    {
        if (!_areas.TryAdd(areaType, areaType)) return;

        var ns = areaType.Namespace + ".Controllers";
        var areaName = areaType.Name.TrimEnd("Area");
        XTrace.WriteLine("开始注册权限管理区域[{0}]，控制器命名空间[{1}]", areaName, ns);

        // 更新区域名集合
        var rs = CubeService.AreaNames?.ToList() ?? new List<String>();
        if (!rs.Contains(areaName))
        {
            rs.Add(areaName);
            CubeService.AreaNames = rs.ToArray();
        }

        // 自动检查并添加菜单
        var task = Task.Run(() =>
        {
            using var span = DefaultTracer.Instance?.NewSpan(nameof(ScanController), areaType.FullName);
            try
            {
                ScanController(areaType, app);
            }
            catch (Exception ex)
            {
                span?.SetError(ex, null);
                XTrace.WriteException(ex);
            }
        });
        task.Wait(5_000);
    }

    /// <summary>自动扫描控制器，并添加到菜单</summary>
    /// <remarks>默认操作当前注册区域的下一级Controllers命名空间</remarks>
    protected static void ScanController(Type areaType, IApplicationBuilder app = null)
    {
        var areaName = areaType.Name.TrimEnd("Area");
        XTrace.WriteLine("start------初始化[{0}]的菜单体系------start", areaName);

        // 初始化数据库
        _ = Menu.Meta.Count;

        // 所有控制器
        var types = areaType.Assembly.GetTypes().Where(e => e.Name.EndsWith("Controller") && e.Namespace == areaType.Namespace + ".Controllers").ToList();

        ScanMenu(types, areaName, app);
    }

    public static void SetRoute<T>(string AreaName, IApplicationBuilder app = null)
    {
        var areaType = typeof(T);

        // 获取控制器
        var types = areaType.Assembly.GetTypes().Where(e => e.Name.EndsWith("Controller") && e.Namespace == areaType.Namespace).ToList();

        ScanMenu(types, AreaName, app);
        ScanRout(AreaName, types, app);
    }

    /// <summary>
    /// 设置路由
    /// </summary>
    /// <param name="areaName"></param>
    /// <param name="types"></param>
    public static void ScanRout(string areaName, List<Type> types, IApplicationBuilder app = null)
    {
        foreach (var type in types)
        {
            var name = type.Name.TrimEnd("Controller");

            foreach (var method in type.GetMethods())
            {
                if (method.IsStatic || !method.IsPublic || method.IsVirtual) continue;
                if (!method.ReturnType.As<IActionResult>() && !method.ReturnType.As<Task<IActionResult>>()) continue;

                var hget = method.GetCustomAttribute<HttpGetAttribute>();

                #region 有HttpGet
                if (hget != null)
                {
                    var atemplates = hget.Template?.Replace("[controller]", name, StringComparison.OrdinalIgnoreCase).Replace("[action]", method.Name, StringComparison.OrdinalIgnoreCase).ToLower().Replace("/Index", "", StringComparison.OrdinalIgnoreCase);
                    if (atemplates.SafeString().StartsWith("/"))
                    {
                        var model = SystemRout.FindByRTypeAndUrl(1, atemplates);
                        if (model == null) model = new SystemRout();
                        model.Name = method.GetDisplayName();
                        model.AreaName = areaName;
                        model.Url = atemplates.Replace("/Index", "", StringComparison.OrdinalIgnoreCase);
                        model.RType = 1;
                        model.ControllerName = name;
                        model.ActionName = method.Name;

                        model.Save();

                        continue;
                    }
                    else
                    {
                        var ss = type.GetCustomAttribute<RouteAttribute>();
                        if (ss != null)
                        {
                            var ctemplates = ss.Template?.Replace("[controller]", name, StringComparison.OrdinalIgnoreCase).Replace("[action]", method.Name.ToLower(), StringComparison.OrdinalIgnoreCase).Replace("[area]", areaName, StringComparison.OrdinalIgnoreCase).ToLower();
                            var url = (ctemplates.SafeString() + "/" + atemplates).Replace("/Index", "", StringComparison.OrdinalIgnoreCase);

                            var model = SystemRout.FindByRTypeAndUrl(1, url);
                            if (model == null) new SystemRout();
                            model.Name = method.GetDisplayName();
                            model.AreaName = areaName;
                            model.Url = url;
                            model.RType = 1;
                            model.ControllerName = name;
                            model.ActionName = method.Name;
                            model.Save();

                            continue;
                        }
                    }
                }
                #endregion

                var sss = type.GetCustomAttribute<RouteAttribute>();

                if (sss != null)
                {
                    var ctemplates = sss.Template?.Replace("[controller]", name, StringComparison.OrdinalIgnoreCase).Replace("[action]", method.Name, StringComparison.OrdinalIgnoreCase).Replace("[area]", areaName, StringComparison.OrdinalIgnoreCase).ToLower();

                    if (!ctemplates.StartsWith('/'))
                    {
                        ctemplates = "/" + ctemplates;
                    }

                    if (!sss.Template.SafeString().Contains("[action]", StringComparison.OrdinalIgnoreCase))
                    {
                        ctemplates = ctemplates + "/" + method.Name.ToLower();
                    }

                    ctemplates = ctemplates.Replace("/Index", "", StringComparison.OrdinalIgnoreCase);

                    var model = SystemRout.FindByRTypeAndUrl(1, ctemplates);
                    if (model == null) model = new SystemRout();
                    model.Name = method.GetDisplayName();
                    model.AreaName = areaName;
                    model.Url = ctemplates;
                    model.RType = 1;
                    model.ControllerName = name;
                    model.ActionName = method.Name;
                    model.Save();
                }
                else
                {
                    var url = ("/" + areaName + "/" + name + "/" + method.Name).ToLower().Replace("/Index", "", StringComparison.OrdinalIgnoreCase);

                    var model = SystemRout.FindByRTypeAndUrl(1, url);
                    if (model == null) model = new SystemRout();
                    model.Name = method.GetDisplayName();
                    model.AreaName = areaName;
                    model.Url = url;
                    model.RType = 1;
                    model.ControllerName = name;
                    model.ActionName = method.Name;
                    model.Save();
                }
            }
        }
    }

    /// <summary>
    /// 扫描菜单
    /// </summary>
    /// <param name="types"></param>
    /// <param name="areaName">区域名称</param>
    /// <param name="app"></param>
    public static void ScanMenu(List<Type> types, String areaName = null, IApplicationBuilder app = null)
    {
        var list = new List<IMenu>();

        var menuList = types.Where(e =>
        {
            var t = e.GetCustomAttribute<DHMenu>();
            return t != null && t.ParentMenuName.SafeString().Length > 0;
        });

        Menu ParentModel = null;

        IEventPublisher _eventPublisher;
        if (app != null)
        {
            _eventPublisher = app.ApplicationServices.GetRequiredService<IEventPublisher>();
        }
        else
        {
            _eventPublisher = EngineContext.Current.Resolve<IEventPublisher>();
        }

        foreach (var type in menuList.ToArray())
        {
            var menuattr = type.GetCustomAttribute<DHMenu>();

            ParentModel = Menu.FindByName(menuattr.ParentMenuName);
            if (ParentModel == null)
            {
                if (menuattr.ParentMenuDisplayName.SafeString().Length == 0)
                {
                    var t = menuList.Where(e =>
                    {
                        var t = e.GetCustomAttribute<DHMenu>();
                        return t.ParentMenuName == menuattr.ParentMenuName && t.ParentMenuDisplayName.SafeString().Length > 0;
                    }).FirstOrDefault();
                    if (t != null)
                    {
                        var menuattr1 = t.GetCustomAttribute<DHMenu>();
                        ParentModel = new Menu();
                        ParentModel.Name = menuattr1.ParentMenuName;
                        ParentModel.DisplayName = menuattr1.ParentMenuDisplayName;
                        ParentModel.FullName = "";
                        ParentModel.ParentID = 0;
                        ParentModel.Sort = menuattr1.ParentMenuOrder;
                        ParentModel.Visible = menuattr1.ParentVisible;
                        ParentModel.Icon = menuattr1.ParentIcon;

                        if (menuattr1.ParentMenuUrl.SafeString().Length > 0)
                        {
                            if (areaName != null)
                            {
                                ParentModel.Url = menuattr1.ParentMenuUrl.Replace("{area}", areaName).ToLower();
                            }
                            else
                            {
                                ParentModel.Url = menuattr1.ParentMenuUrl.Replace("{area}", AdminArea.AreaName).ToLower();
                            }
                        }

                        ParentModel.Permission = "";
                        ParentModel.Insert();

                        // 消费菜单生成
                        _eventPublisher.Publish(new MenuEvent(ParentModel, menuattr1.Expand));

                        list.Add(ParentModel);
                    }
                }
                else
                {
                    ParentModel = new Menu();
                    ParentModel.Name = menuattr.ParentMenuName;
                    ParentModel.DisplayName = menuattr.ParentMenuDisplayName;
                    ParentModel.FullName = "";
                    ParentModel.ParentID = 0;
                    ParentModel.Sort = menuattr.ParentMenuOrder;
                    ParentModel.Visible = menuattr.ParentVisible;
                    ParentModel.Icon = menuattr.ParentIcon;

                    if (menuattr.ParentMenuUrl.SafeString().Length > 0)
                    {
                        if (areaName != null)
                        {
                            ParentModel.Url = menuattr.ParentMenuUrl.Replace("{area}", areaName).ToLower();
                        }
                        else
                        {
                            ParentModel.Url = menuattr.ParentMenuUrl.Replace("{area}", AdminArea.AreaName).ToLower();
                        }
                    }

                    ParentModel.Permission = "";
                    ParentModel.Insert();

                    // 消费菜单生成
                    _eventPublisher.Publish(new MenuEvent(ParentModel, menuattr.Expand));

                    list.Add(ParentModel);
                }
            }

            if (ParentModel == null) continue;

            var name = menuattr.CurrentMenuName;

            var modelmenu = Menu.FindByName(name);
            if (modelmenu == null) modelmenu = new Menu();
            modelmenu.Name = name;
            modelmenu.DisplayName = type.GetDisplayName();
            modelmenu.FullName = type.FullName;
            modelmenu.ParentID = ParentModel.ID;

            if (areaName != null)
            {
                modelmenu.Url = menuattr.CurrentMenuUrl.Replace("{area}", areaName).ToLower();
            }
            else
            {
                modelmenu.Url = menuattr.CurrentMenuUrl.Replace("{area}", AdminArea.AreaName).ToLower();
            }

            modelmenu.Remark = type.GetDescription();
            modelmenu.Icon = menuattr.CurrentIcon.SafeString();

            if (modelmenu.ID == 0)
            {
                modelmenu.Visible = menuattr.CurrentVisible;
            }

            // 排序
            if (modelmenu.Sort == 0)
            {
                var pi = type.GetPropertyEx("MenuOrder");
                if (pi != null) modelmenu.Sort = pi.GetValue(null).ToInt();
            }

            modelmenu = ScanActionMenu(type, modelmenu);

            if (modelmenu.ID > 0)
            {
                modelmenu.Update();

                // 消费菜单生成
                _eventPublisher.Publish(new MenuEvent(modelmenu, menuattr.Expand));
            }
            else
            {
                modelmenu.Insert();

                // 消费菜单生成
                _eventPublisher.Publish(new MenuEvent(modelmenu, menuattr.Expand));
            }

            list.Add(modelmenu);
        }

        // 如果新增了菜单，需要检查权限
        if (list.Count > 0)
        {
            var task = Task.Run(() =>
            {
                XTrace.WriteLine("新增了菜单，需要检查权限");

                var fact = ManageProvider2.GetFactory<IRole>();
                fact.EntityType.Invoke("CheckRole");
            });
            task.Wait(5_000);
        }
    }

    /// <summary>获取可用于生成权限菜单的Action集合</summary>
    /// <param name="type">控制器</param>
    /// <param name="menu">菜单</param>
    /// <returns></returns>
    private static Menu ScanActionMenu(Type type, Menu menu)
    {
        var dic = new Dictionary<MethodInfo, Int32>();

        // 添加该类型下的所有Action
        foreach (var method in type.GetMethods())
        {
            if (method.IsStatic || !method.IsPublic || method.IsVirtual) continue;
            if (!method.ReturnType.As<IActionResult>() && !method.ReturnType.As<Task<IActionResult>>()) continue;

            if (method.GetCustomAttribute<AllowAnonymousAttribute>() != null) continue;

            var att = method.GetCustomAttribute<EntityAuthorizeAttribute>();
            if (att != null && att.Permission > PermissionFlags.None) dic.Add(method, (Int32)att.Permission);
        }

        if (dic == null || dic.Count == 0) return menu;

        // 添加该类型下的所有Action作为可选权限子项
        foreach (var item in dic)
        {
            var method = item.Key;

            var dn = method.GetDisplayName();
            if (!dn.IsNullOrEmpty()) dn = dn.Replace("{type}", menu?.FriendName);

            var pmName = !dn.IsNullOrEmpty() ? dn : method.Name;
            if (item.Value <= (Int32)PermissionFlags.Delete) pmName = ((PermissionFlags)item.Value).GetDescription();
            menu.Permissions[item.Value] = pmName;
        }

        return menu;
    }

    private static ICollection<String> _namespaces;

    /// <summary>判断控制器是否归属于后台管辖</summary>
    /// <param name="controllerActionDescriptor"></param>
    /// <returns></returns>
    public static Boolean Contains(ControllerActionDescriptor controllerActionDescriptor)
    {
        // 判断控制器是否在管辖范围之内
        var controller = controllerActionDescriptor.ControllerTypeInfo;
        var ns = controller.Namespace;
        if (!ns.EndsWith(".Controllers")) return false;

        if (_namespaces == null) _namespaces = new HashSet<String>(_areas.Keys.Select(e => e.Namespace));

        // 该控制器父级命名空间必须有对应的区域注册类，才会拦截其异常
        ns = ns.TrimEnd(".Controllers");
        return _namespaces.Contains(ns);
    }
}