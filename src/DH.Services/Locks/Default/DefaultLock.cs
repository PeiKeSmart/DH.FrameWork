using NewLife.Caching;

namespace DH.Services.Locks.Default;

/// <summary>
/// 业务锁
/// </summary>
public class DefaultLock : ILock {
    /// <summary>
    /// 缓存提供程序
    /// </summary>
    private readonly ICache _cache;

    /// <summary>
    /// 锁定标识
    /// </summary>
    private String _key;

    /// <summary>
    /// 延迟执行时间
    /// </summary>
    private Int32 _expiration;

    /// <summary>
    /// 初始化一个<see cref="DefaultLock"/>类型的实例
    /// </summary>
    /// <param name="cache">缓存提供程序</param>
    public DefaultLock(ICache cache) => _cache = cache;

    /// <summary>
    /// 锁定，成功锁定返回true，false代表之前已被锁定
    /// </summary>
    /// <param name="key">锁定标识</param>
    /// <param name="expiration">锁定时间间隔</param>
    public bool Lock(String key, Int32 expiration)
    {
        _key = key;
        _expiration = expiration;
        if (_cache.ContainsKey(key))
            return false;
        return _cache.Set(key, 1, expiration);
    }

    /// <summary>
    /// 解除锁定
    /// </summary>
    public void UnLock(Boolean autoUnLock)
    {
        if (autoUnLock) _cache.Remove(_key);

        if (_expiration != 0)
            return;
        if (!_cache.ContainsKey(_key))
            return;
        _cache.Remove(_key);
    }
}