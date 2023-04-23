namespace DH.Caching;

/// <summary>
/// 缓存键值集合
/// </summary>
public static class CacheKeyCollection {
    /// <summary>
    /// 站点设置
    /// </summary>
    public const string SiteSettings = "Cache-SiteSettings";

    /// <summary>
    /// 登录错误缓存
    /// </summary>
    /// <param name="username">出错时用户名</param>
    /// <returns></returns>
    public static string ManagerLoginError(string username)
    {
        return string.Format("Cache-Manager-Login-{0}", username);
    }

    /// <summary>
    /// 登录错误时的时间缓存
    /// </summary>
    /// <param name="username">出错时用户名</param>
    /// <returns></returns>
    public static string ManagerLoginTimeError(string username)
    {
        return string.Format("Cache-Manager-LoginTime-{0}", username);
    }

}