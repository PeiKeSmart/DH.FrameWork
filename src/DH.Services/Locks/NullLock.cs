namespace DH.Services.Locks;

/// <summary>
/// 空业务锁
/// </summary>
public class NullLock : ILock {
    /// <summary>
    /// 空业务锁
    /// </summary>
    public static ILock Instance { get; } = new NullLock();

    /// <summary>
    /// 初始化一个<see cref="NullLock"/>类型的实例
    /// </summary>
    private NullLock() { }

    /// <summary>
    /// 锁定，成功锁定返回true，false代表之前已被锁定
    /// </summary>
    /// <param name="key">锁定标识</param>
    /// <param name="expiration">锁定时间间隔</param>
    public bool Lock(String key, Int32 expiration) => true;

    /// <summary>
    /// 解除锁定
    /// </summary>
    public void UnLock() { }
}