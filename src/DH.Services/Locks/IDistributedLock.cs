namespace DH.Services.Locks;

/// <summary>
/// 分布式锁
/// </summary>
public interface IDistributedLock {
    /// <summary>
    /// 尝试获取锁。成功锁定返回true, false代表之前已被锁定
    /// </summary>
    /// <param name="key">锁定标识</param>
    /// <param name="expiration">锁定时间间隔</param>
    /// <returns></returns>
    Boolean TryLock(String key, TimeSpan? expiration = null);

    /// <summary>
    /// 锁定。如果锁空闲立即返回，否则一直等待
    /// </summary>
    /// <param name="key">锁定标识</param>
    void Lock(String key);

    /// <summary>
    /// 尝试批量获取锁。成功锁定返回true, false代表之前已被锁定
    /// </summary>
    /// <param name="keys">批量锁定标识</param>
    /// <param name="expiration">锁定时间间隔</param>
    /// <returns></returns>
    Boolean TryLock(List<String> keys, TimeSpan? expiration = null);

    /// <summary>
    /// 解除锁定
    /// </summary>
    /// <param name="key"></param>
    void UnLock(String key);

    /// <summary>
    /// 批量解除锁定
    /// </summary>
    /// <param name="keys">锁定标识列表</param>
    void UnLock(List<String> keys);
}