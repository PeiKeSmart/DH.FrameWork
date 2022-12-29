using DH.Web.Framework.Common;

using System.ComponentModel;

namespace DH.Web.Framework.Admin;

/// <summary>权限管理区域注册</summary>
[DisplayName("系统管理")]
public class AdminArea : AreaBase
{
    /// <summary>区域名</summary>
    public static String AreaName => DHSetting.Current.AdminArea;

    public AdminArea() : base(AreaName) { }
}