using DH.Model;
using DH.Web.Framework;
using DH.Web.Framework.Admin;

using Microsoft.AspNetCore.Mvc;

using System.ComponentModel;

namespace UIDemo1.Areas.Admin.Controllers;

/// <summary>控制台</summary>
[DisplayName("控制台")]
[Description("后台登录之后的首页")]
[AdminArea]
[DHMenu(ParentMenuName = "Home", ParentMenuDisplayName = "首页", ParentMenuUrl = "~/{area}/Home/Console", ParentMenuOrder = 100, CurrentMenuUrl = "~/{area}/Home/Console", CurrentMenuName = "Console")]
public class HomeController : BaseAdminControllerX
{
    public IActionResult Index()
    {
        return View();
    }
}
