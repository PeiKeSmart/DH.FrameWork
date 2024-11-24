using DH.Core.Configuration;

using NewLife.Model;

using Pek.Infrastructure;

namespace DH.Core.Caching;

/// <summary>
/// 表示缓存对象的键
/// </summary>
public partial class CacheKey
{
    #region 初始化

    /// <summary>
    /// 使用键和前缀初始化新实例
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="prefixes">按前缀删除功能的前缀</param>
    public CacheKey(string key, params string[] prefixes)
    {
        Key = key;
        Prefixes.AddRange(prefixes.Where(prefix => !string.IsNullOrEmpty(prefix)));
    }

    #endregion

    #region 方法

    /// <summary>
    /// 从当前实例创建一个新实例，并用传递的参数填充它
    /// </summary>
    /// <param name="createCacheKeyParameters">用于创建参数的函数</param>
    /// <param name="keyObjects">要创建参数的对象</param>
    /// <returns>缓存密钥</returns>
    public virtual CacheKey Create(Func<object, object> createCacheKeyParameters, params object[] keyObjects)
    {
        var cacheKey = new CacheKey(Key, Prefixes.ToArray());

        if (!keyObjects.Any())
            return cacheKey;

        cacheKey.Key = string.Format(cacheKey.Key, keyObjects.Select(createCacheKeyParameters).ToArray());

        for (var i = 0; i < cacheKey.Prefixes.Count; i++)
            cacheKey.Prefixes[i] = string.Format(cacheKey.Prefixes[i], keyObjects.Select(createCacheKeyParameters).ToArray());

        return cacheKey;
    }

    #endregion

    #region 属性

    /// <summary>
    /// 获取或设置缓存键
    /// </summary>
    public string Key { get; protected set; }

    /// <summary>
    /// 获取或设置按前缀删除功能的前缀
    /// </summary>
    public List<string> Prefixes { get; protected set; } = new List<string>();

    /// <summary>
    /// 获取或设置缓存时间（分钟）
    /// </summary>
    public int CacheTime { get; set; } = ObjectContainer.Provider.GetPekService<AppSettings>().Get<CacheConfig>().DefaultCacheTime;

    #endregion

    #region 嵌套类

    public class CacheKeyEqualityComparer : IEqualityComparer<CacheKey>
    {
        public bool Equals(CacheKey x, CacheKey y)
        {
            if (x == null && y == null)
                return true;

            return x?.Key.Equals(y?.Key, StringComparison.OrdinalIgnoreCase) ?? false;
        }

        public int GetHashCode(CacheKey obj)
        {
            return obj.Key.GetHashCode();
        }
    }

    #endregion
}
