namespace DH.Model;

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
}