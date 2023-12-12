using NewLife.Caching;

namespace DH.Caching;

/// <summary>
/// 缓存Key字段
/// </summary>
public class CacheKeyFiled {
    /// <summary>缓存Key集合</summary>
    protected ICache CacheKey;

    /// <summary>默认缓存字段</summary>
    public static CacheKeyFiled Instance { get; set; } = new CacheKeyFiled();

    public CacheKeyFiled()
    {
        CacheKey = new MemoryCache { Period = 60 };
    }

    /// <summary>获取Key组合名称</summary>
    public String Get(String Key)
    {
        if (String.IsNullOrWhiteSpace(Key))
        {
            throw new ArgumentNullException();
        }

        return DHUtilSetting.Current.CacheKeyPrefix + Key;
    }
}