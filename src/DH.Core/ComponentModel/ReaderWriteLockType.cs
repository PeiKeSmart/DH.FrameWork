namespace DH.Core.ComponentModel
{
    /// <summary>
    /// 读写器锁类型
    /// </summary>
    public enum ReaderWriteLockType
    {
        Read,
        Write,
        UpgradeableRead
    }
}
