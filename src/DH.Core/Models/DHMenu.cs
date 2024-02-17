namespace DH.Models;

/// <summary>菜单模式</summary>
[Flags]
public enum MenuModes {
    /// <summary>管理后台可见</summary>
    Admin = 1,

    /// <summary>租户可见</summary>
    Tenant = 2,
}

/// <summary>
/// 菜单属性
/// </summary>
public class DHMenu : Attribute
{
    /// <summary>
    /// 父菜单中文名称
    /// </summary>
    public String ParentMenuDisplayName { get; set; }

    /// <summary>
    /// 父菜单默认跳转地址
    /// </summary>
    public String ParentMenuUrl { get; set; }

    /// <summary>
    /// 父菜单英文名称
    /// </summary>
    public String ParentMenuName { get; set; }

    /// <summary>
    /// 父菜单排序
    /// </summary>
    public Int32 ParentMenuOrder { get; set; }

    /// <summary>
    /// 父菜单图标
    /// </summary>
    public String ParentIcon { get; set; }

    /// <summary>
    /// 当前菜单显示隐藏
    /// </summary>
    public Boolean ParentVisible { get; set; } = true;

    /// <summary>
    /// 当前菜单地址
    /// </summary>
    public String CurrentMenuUrl { get; set; }

    /// <summary>
    /// 当前菜单英文名称
    /// </summary>
    public String CurrentMenuName { get; set; }

    /// <summary>
    /// 当前菜单显示隐藏
    /// </summary>
    public Boolean CurrentVisible { get; set; } = true;

    /// <summary>
    /// 当前菜单图标
    /// </summary>
    public String CurrentIcon { get; set; }

    /// <summary>
    /// 扩展字段
    /// </summary>
    public String Expand { get; set; }

    /// <summary>菜单模式。控制在管理后台和租户模式下是否可见</summary>
    public MenuModes Mode { get; set; }

    /// <summary>最后更新时间。小于该更新时间的菜单设置将被覆盖。</summary>
    /// <remarks>一般应用于区域类，表明代码已修改菜单参数，希望强行覆盖已有菜单设置</remarks>
    public String LastUpdate { get; set; }
}