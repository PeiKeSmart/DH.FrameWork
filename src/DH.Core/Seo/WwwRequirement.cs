namespace DH.Seo;

/// <summary>
/// 表示 WWW 要求
/// </summary>
public enum WwwRequirement {
    /// <summary>
    /// 没关系（什么都不做）
    /// </summary>
    NoMatter = 0,

    /// <summary>
    /// 页面应具有 WWW 前缀
    /// </summary>
    WithWww = 10,

    /// <summary>
    /// 页面不应具有 WWW 前缀
    /// </summary>
    WithoutWww = 20
}