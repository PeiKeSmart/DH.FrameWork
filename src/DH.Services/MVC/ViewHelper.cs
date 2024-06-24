using DH.AspNetCore.Extensions;
using DH.AspNetCore.ViewModels;
using DH.Common;
using DH.Entity;
using DH.Models;
using DH.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;

using NewLife;

using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;

namespace DH.MVC;

/// <summary>视图助手</summary>
public static class ViewHelper {
    /// <summary>创建页面设置的委托</summary>
    public static Func<Bootstrap> CreateBootstrap = () => new Bootstrap();

    /// <summary>获取页面设置</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static Bootstrap Bootstrap(this HttpContext context)
    {
        var bs = context.Items["Bootstrap"] as Bootstrap;
        if (bs == null)
        {
            bs = CreateBootstrap();
            context.Items["Bootstrap"] = bs;
        }

        return bs;
    }

    /// <summary>获取页面设置</summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    public static Bootstrap Bootstrap(this Controller controller) => Bootstrap(controller.HttpContext);

    /// <summary>获取路由Key</summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static RouteValueDictionary GetRouteKey(this IEntity entity)
    {
        var fact = EntityFactory.CreateFactory(entity.GetType());
        var pks = fact.Table.PrimaryKeys;

        var rv = new RouteValueDictionary();
        if (fact.Unique != null)
        {
            rv["id"] = entity[fact.Unique.Name];
        }
        else if (pks.Length > 0)
        {
            foreach (var item in pks)
            {
                rv[item.Name] = entity[item.Name];
            }
        }

        return rv;
    }

    /// <summary>获取排序分页以外的参数</summary>
    /// <returns></returns>
    public static RouteValueDictionary GetRouteValue(this Pager page)
    {
        var dic = new RouteValueDictionary();
        foreach (var item in page.Params)
        {
            if (!item.Key.EqualIgnoreCase("Sort", "Desc", "PageIndex", "PageSize")) dic[item.Key] = item.Value;
        }

        return dic;
    }

    /// <summary>是否启用多选</summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public static Boolean EnableSelect(this IRazorPage page)
    {
        if (page.ViewContext.ViewData.TryGetValue("EnableSelect", out var rs)) return (Boolean)rs;

        return page.Has(PermissionFlags.Update, PermissionFlags.Delete);
    }

    /// <summary>获取头像地址</summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static String GetAvatarUrl(this IUser user)
    {
        if (user == null || user.Avatar.IsNullOrEmpty()) return null;

        // 绝对路径
        if (user.Avatar.StartsWithIgnoreCase("http://", "https://", "/")) return user.Avatar;

        var set = DHSetting.Current;

        if (!user.Avatar.StartsWithIgnoreCase("/Sso/"))
        {
            var av = set.AvatarPath.CombinePath(user.Avatar).GetBasePath();
            if (File.Exists(av)) return "/Cube/Avatar?id=" + user.ID;
        }

        // 兼容旧版头像
        if (!set.AvatarPath.IsNullOrEmpty())
        {
            var av = set.AvatarPath.CombinePath(user.ID + ".png").GetBasePath();
            if (File.Exists(av)) return "/Sso/Avatar?id=" + user.ID;
        }

        return null;
    }

    /// <summary>
    /// 根据文件名称获取文件地址
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static String GetFileUrl(this String filename)
    {
        if (filename.IsNullOrEmpty()) return null;

        var set = DHSetting.Current;
        if (!filename.IsNullOrEmpty())
        {
            // 修正资源访问起始路径
            var file = set.UploadPath.CombinePath(filename).GetBasePath();
            if (File.Exists(file)) return set.UploadPath.CombinePath(filename);
        }

        return null;
    }

    private static Boolean? _IsDevelop;
    /// <summary>当前是否开发环境。判断csproj文件</summary>
    /// <returns></returns>
    public static Boolean IsDevelop()
    {
        if (_IsDevelop != null) return _IsDevelop.Value;

        var di = ".".AsDirectory();
        if (!di.Exists)
            _IsDevelop = false;
        else
        {
            var fis = di.GetFiles("*.csproj", SearchOption.TopDirectoryOnly);
            _IsDevelop = fis != null && fis.Length > 0;
        }

        return _IsDevelop.Value;
    }

    private static readonly Dictionary<String, String> _logo_cache = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
    /// <summary>获取指定名称的Logo图标</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static String GetLogo(String name)
    {
        if (_logo_cache.TryGetValue(name, out var logo)) return logo;

        //var ico = "/Content/images/logo/{0}.png".F(mi.Name);
        var paths = new[] { "/Content/images/logo/", "/Content/Logo/" };
        foreach (var item in paths)
        {
            var p = item.TrimStart("/");
            p = DHSetting.Current.WebRootPath.CombinePath(p);

            var di = p.AsDirectory();
            if (di.Exists)
            {
                var ico = di.GetAllFiles(name + ".*").FirstOrDefault();
                if (ico != null && ico.Exists)
                {
                    logo = item + ico.Name;
                    break;
                }
            }
        }

        // 缓存起来
        _logo_cache[name] = logo;

        return logo;
    }

    /// <summary>
    /// 获取用户所拥有的菜单
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static IList<MenuTree> GetMenus(this IUser user)
    {
        if (user == null) user = User.FindAll().FirstOrDefault();

        var fact = ManageProvider.Menu;
        var menus = fact.Root.Childs;
        if (user?.Role != null)
        {
            menus = fact.GetMySubMenus(fact.Root.ID, user, true);
        }

        // 如果顶级只有一层，并且至少有三级目录，则提升一级
        if (menus.Count == 1 && menus[0].Childs.All(m => m.Childs.Count > 0)) { menus = menus[0].Childs; }

        var menuTree = MenuTree.GetMenuTree(pMenuTree =>
        {
            var subMenus = fact.GetMySubMenus(pMenuTree.ID, user, true);
            return subMenus;
        }, list =>
        {

            var menuList = (from menu in list
                                // where m.Visible
                            select new MenuTree
                            {
                                ID = menu.ID,
                                Name = menu.Name,
                                DisplayName = menu.DisplayName ?? menu.Name,
                                FullName = menu.FullName,
                                Url = menu.Url,
                                Icon = menu.Icon,
                                Visible = menu.Visible,
                                NewWindow = menu.NewWindow,
                                ParentID = menu.ParentID,
                                Permissions = menu.Permissions
                            }).ToList();
            return menuList.Count > 0 ? menuList : null;
        }, menus);

        return menuTree;
    }

    /// <summary>获取附件Url</summary>
    /// <param name="attachment"></param>
    /// <returns></returns>
    public static String GetAttachmentUrl(Attachment attachment)
    {
        if (!attachment.ContentType.IsNullOrEmpty() && attachment.ContentType.StartsWithIgnoreCase("image/"))
            return $"/cube/image/{attachment.Id}{attachment.Extension}";

        return $"/cube/file/{attachment.Id}{attachment.Extension}";
    }

    /// <summary>是否附件列</summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public static Boolean IsAttachment(this IDataColumn dc) => dc.ItemType.EqualIgnoreCase("file", "image") || dc.ItemType.StartsWithIgnoreCase("file-", "image-");
}

/// <summary>Bootstrap页面控制。允许继承</summary>
public class Bootstrap {
    #region 属性
    /// <summary>最大列数</summary>
    public Int32 MaxColumn { get; set; } //= 2;

    /// <summary>默认标签宽度</summary>
    public Int32 LabelWidth { get; set; }// = 4;
    #endregion

    #region 当前项
    ///// <summary>当前项</summary>
    //public FieldItem Item { get; set; }

    /// <summary>名称</summary>
    public String Name { get; set; }

    /// <summary>类型</summary>
    public Type Type { get; set; }

    /// <summary>长度</summary>
    public Int32 Length { get; set; }

    /// <summary>设置项</summary>
    public void Set(FieldItem item)
    {
        Name = item.Name;
        Type = item.Type;
        Length = item.Length;
    }
    #endregion

    #region 构造
    /// <summary>实例化一个页面助手</summary>
    public Bootstrap()
    {
        MaxColumn = 2;
        LabelWidth = 4;
    }
    #endregion

    #region 方法
    /// <summary>获取分组宽度</summary>
    /// <returns></returns>
    public virtual Int32 GetGroupWidth()
    {
        if (MaxColumn > 1 && Type != null)
        {
            if (Type != typeof(String) || Length <= 100) return 12 / MaxColumn;
        }

        return 12;
    }
    #endregion
}