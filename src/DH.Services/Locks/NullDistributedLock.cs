namespace DH.Services.Locks;

/// <summary>
/// 空分布式锁
/// </summary>
public class NullDistributedLock : IDistributedLock {
    /// <summary>
    /// 空分布式锁
    /// </summary>
    public static IDistributedLock Instance { get; } = new NullDistributedLock();

    /// <summary>
    /// 初始化一个<see cref="NullDistributedLock"/>类型的实例
    /// </summary>
    private NullDistributedLock() { }

    /// <summary>
    /// 尝试获取锁。成功锁定返回true，false代表之前已被锁定
    /// </summary>
    /// <param name="key">锁定标识</param>
    /// <param name="expiration">锁定时间间隔</param>
    /// <returns></returns>
    public Boolean TryLock(String key, TimeSpan? expiration = null) => true;

    /// <summary>
    /// 锁定。如果锁空闲立即返回，否则一直等待
    /// </summary>
    /// <param name="key">锁定标识</param>
    public void Lock(String key)
    {
    }

    /// <summary>
    /// 尝试批量获取锁。成功锁定返回true，false代表之前已被锁定
    /// </summary>
    /// <param name="keys">批量锁定标识</param>
    /// <param name="expiration">锁定时间间隔</param>
    public bool TryLock(List<String> keys, TimeSpan? expiration = null) => true;

    /// <summary>
    /// 解除锁定
    /// </summary>
    /// <param name="key">锁定标识</param>
    public void UnLock(String key)
    {
    }

    /// <summary>
    /// 批量解除锁定
    /// </summary>
    /// <param name="keys">锁定标识列表</param>
    public void UnLock(List<String> keys)
    {
    }
}