namespace DH.Core.Domain.Seo;

/// <summary>
/// 表示WWW要求
/// </summary>
public enum WwwRequirement
{
    /// <summary>
    /// 没关系（什么都不做）
    /// </summary>
    NoMatter = 0,

    /// <summary>
    /// 页面应具有WWW前缀
    /// </summary>
    WithWww = 10,

    /// <summary>
    /// 页面不应具有WWW前缀
    /// </summary>
    WithoutWww = 20
}
