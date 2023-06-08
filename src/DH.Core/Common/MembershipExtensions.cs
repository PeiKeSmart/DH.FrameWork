﻿using Microsoft.AspNetCore.Mvc.Razor;

using XCode.Membership;

namespace DH.Common;

/// <summary>用户扩展</summary>
public static class MembershipExtensions {
    /// <summary>用户只有拥有当前菜单的指定权限</summary>
    /// <param name="page">页面</param>
    /// <param name="flags">是否拥有多个权限中的任意一个，或的关系。如果需要表示与的关系，可以传入一个多权限位合并</param>
    /// <returns></returns>
    public static Boolean Has(this IRazorPage page, params PermissionFlags[] flags)
    {
        // 没有用户时无权
        var user = page.ViewContext.ViewBag.User as IUser ??
            page.ViewContext.HttpContext.User.Identity as IUser ??
            Thread.CurrentPrincipal?.Identity as IUser;
        if (user == null) return false;

        // 没有菜单时不做权限控制
        var menu = page.ViewContext.ViewBag.Menu as IMenu;
        menu ??= page.ViewContext.HttpContext.Items["CurrentMenu"] as IMenu;
        if (menu == null) return true;

        return user.Has(menu, flags);
    }

    /// <summary>用户只有拥有当前菜单的指定权限</summary>
    /// <param name="page">页面</param>
    /// <param name="menu">菜单</param>
    /// <param name="flags">是否拥有多个权限中的任意一个，或的关系。如果需要表示与的关系，可以传入一个多权限位合并</param>
    /// <returns></returns>
    public static Boolean Has(this IRazorPage page, IMenu menu, params PermissionFlags[] flags)
    {
        // 没有菜单时不做权限控制
        if (menu == null) return true;

        // 没有用户时无权
        var user = page.ViewContext.ViewBag.User as IUser ??
            page.ViewContext.HttpContext.User.Identity as IUser ??
            Thread.CurrentPrincipal?.Identity as IUser;
        if (user == null) return false;

        return user.Has(menu, flags);
    }
}