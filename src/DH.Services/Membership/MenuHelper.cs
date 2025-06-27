using System.Reflection;

using DH.Models;

using NewLife;
using NewLife.Log;
using NewLife.Reflection;

using Pek.Models;

using XCode;
using XCode.Membership;

namespace DH.Services.Membership;

/// <summary>
/// 菜单助手
/// </summary>
public static class MenuHelper {

    /// <summary>扫描命名空间下的控制器并添加为菜单</summary>
    /// <param name="rootName">根菜单名称，所有菜单附属在其下</param>
    /// <param name="asm">要扫描的程序集</param>
    /// <param name="nameSpace">要扫描的命名空间</param>
    /// <param name="menuFactory">菜单工厂</param>
    /// <returns></returns>
    public static IList<IMenu> ScanController(this IMenuFactory menuFactory, String rootName, Assembly asm, String nameSpace)
    {
        var list = new List<IMenu>();

        // 所有控制器
        var types = asm.GetTypes().Where(e => e.Name.EndsWith("Controller") && e.Namespace == nameSpace).ToList();
        if (types.Count == 0) return list;

        // 如果根菜单不存在，则添加
        var r = menuFactory.Root;
        var root = menuFactory.FindByFullName(nameSpace);
        if (root == null) root = r.FindByPath(rootName);
        //if (root == null) root = r.Childs.FirstOrDefault(e => e.Name.EqualIgnoreCase(rootName));
        //if (root == null) root = r.Childs.FirstOrDefault(e => e.Url.EqualIgnoreCase("~/" + rootName));
        if (root == null)
        {
            root = r.Add(rootName, null, nameSpace, "~/" + rootName);
            list.Add(root);
        }
        if (root.FullName != nameSpace)
        {
            root.FullName = nameSpace;
            (root as IEntity).Save();
        }

        var ms = new List<IMenu>();

        // 遍历该程序集所有类型
        foreach (var type in types)
        {
            var name = type.Name.TrimEnd("Controller");
            var url = root.Url;
            var node = root;

            // 添加Controller
            var controller = node.FindByPath(name);
            if (controller == null)
            {
                url += "/" + name;
                controller = Menu.FindByUrl(url);
                if (controller == null)
                {
                    // DisplayName特性作为中文名
                    controller = node.Add(name, type.GetDisplayName(), type.FullName, url);

                    //list.Add(controller);
                }
            }
            if (controller.FullName.IsNullOrEmpty()) controller.FullName = type.FullName;
            if (controller.Remark.IsNullOrEmpty()) controller.Remark = type.GetDescription();

            ms.Add(controller);
            list.Add(controller);

            // 反射调用控制器的方法来获取动作
            var func = type.GetMethodEx("ScanActionMenu");
            if (func == null) continue;

            // 由于控制器使用IOC，无法直接实例化控制器，需要给各个参数传入空
            var ctor = type.GetConstructors()?.FirstOrDefault();
            var ctrl = ctor.Invoke(new Object[ctor.GetParameters().Length]);
            //var ctrl = type.CreateInstance();

            var acts = func.As<Func<IMenu, IDictionary<MethodInfo, Int32>>>(ctrl).Invoke(controller);
            if (acts == null || acts.Count == 0) continue;

            // 可选权限子项
            controller.Permissions.Clear();

            // 添加该类型下的所有Action作为可选权限子项
            foreach (var item in acts)
            {
                var method = item.Key;

                var dn = method.GetDisplayName();
                if (!dn.IsNullOrEmpty()) dn = dn.Replace("{type}", (controller as Menu)?.FriendName);

                var pmName = !dn.IsNullOrEmpty() ? dn : method.Name;
                if (item.Value <= (Int32)PermissionFlags.Delete) pmName = ((PermissionFlags)item.Value).GetDescription();
                controller.Permissions[item.Value] = pmName;
            }

            // 排序
            if (controller.Sort == 0)
            {
                var pi = type.GetPropertyEx("MenuOrder");
                if (pi != null) controller.Sort = pi.GetValue(null).ToInt();
            }
        }

        for (var i = 0; i < ms.Count; i++)
        {
            (ms[i] as IEntity).Save();
        }

        // 如果新增了菜单，需要检查权限
        if (list.Count > 0)
        {
            ThreadPool.UnsafeQueueUserWorkItem(s =>
            {
                try
                {
                    XTrace.WriteLine("新增了菜单，需要检查权限");
                    //var fact = ManageProvider.GetFactory<IRole>();
                    var fact = typeof(Role).AsFactory();
                    fact.EntityType.Invoke("CheckRole");
                }
                catch (Exception ex)
                {
                    XTrace.WriteException(ex);
                }
            }, null);
        }

        return list;
    }

    /// <summary>根据租户隔离菜单</summary>
    /// <param name="menus"></param>
    /// <param name="isTenant"></param>
    /// <returns></returns>
    public static IList<MenuTree> FilterByTenant(IList<MenuTree> menus, Boolean isTenant)
    {
        var list = new List<MenuTree>();

        foreach (var item in menus)
        {
            if (!item.FullName.IsNullOrEmpty())
            {
                // 控制器菜单是否支持租户显示
                if (CheckVisibleInTenant(item))
                {
                    // 支持租户显示，且当前是租户，则显示
                    if (isTenant)
                        list.Add(item);
                    // 同时支持租户和管理员显示
                    else if (CheckVisibleInAdmin(item))
                        list.Add(item);
                }
                else
                {
                    // 不支持租户显示，且不是租户，则显示
                    if (!isTenant)
                        list.Add(item);
                    else if (item.Children != null)
                    {
                        // 虽然当前大菜单不支持租户显示，但是子菜单支持，则显示
                        if (item.Children.Any(e => CheckVisibleInTenant(e)))
                            list.Add(item);
                    }
                }
            }
        }

        return list;
    }

    static Dictionary<String, Boolean> _tenants = [];
    static Boolean CheckVisibleInTenant(MenuTree menu)
    {
        var key = menu.FullName;
        if (_tenants.TryGetValue(key, out var rs)) return rs;

        var type = Type.GetType(menu.FullName);
        if (type == null)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(menu.FullName);
                if (type != null) break;
            }
        }
        var att = type?.GetCustomAttribute<DHMenuAttribute>();
        if (att != null && att.Mode.Has(MenuModes.Tenant))
        {
            return _tenants[key] = true;
        }

        return _tenants[key] = false;
    }

    static Dictionary<String, Boolean> _admins = [];
    static Boolean CheckVisibleInAdmin(MenuTree menu)
    {
        var key = menu.FullName;
        if (_admins.TryGetValue(key, out var rs)) return rs;

        var type = Type.GetType(menu.FullName);
        var att = type?.GetCustomAttribute<DHMenuAttribute>();
        if (att != null && att.Mode.Has(MenuModes.Admin))
        {
            return _admins[key] = true;
        }

        return _admins[key] = false;
    }
}