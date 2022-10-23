namespace DH.Core.ComponentModel
{
    /// <summary>
    /// 为实现对资源的锁定访问提供了一种方便的方法。
    /// </summary>
    /// <remarks>
    /// 作为基础设施类。
    /// </remarks>
    public partial class ReaderWriteLockDisposable : IDisposable
    {
        private bool _disposed = false;
        private readonly ReaderWriterLockSlim _rwLock;
        private readonly ReaderWriteLockType _readerWriteLockType;

        /// <summary>
        /// 初始化<see cref="ReaderWriteLockDisposable"/>类的新实例。
        /// </summary>
        /// <param name="rwLock">读写器锁</param>
        /// <param name="readerWriteLockType">锁定类型</param>
        public ReaderWriteLockDisposable(ReaderWriterLockSlim rwLock, ReaderWriteLockType readerWriteLockType = ReaderWriteLockType.Write)
        {
            _rwLock = rwLock;
            _readerWriteLockType = readerWriteLockType;

            switch (_readerWriteLockType)
            {
                case ReaderWriteLockType.Read:
                    _rwLock.EnterReadLock();
                    break;
                case ReaderWriteLockType.Write:
                    _rwLock.EnterWriteLock();
                    break;
                case ReaderWriteLockType.UpgradeableRead:
                    _rwLock.EnterUpgradeableReadLock();
                    break;
            }
        }

        // 消费者可调用的Dispose模式的公共实现。
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Dispose模式的受保护实现。
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                switch (_readerWriteLockType)
                {
                    case ReaderWriteLockType.Read:
                        _rwLock.ExitReadLock();
                        break;
                    case ReaderWriteLockType.Write:
                        _rwLock.ExitWriteLock();
                        break;
                    case ReaderWriteLockType.UpgradeableRead:
                        _rwLock.ExitUpgradeableReadLock();
                        break;
                }
            }
            _disposed = true;
        }
    }
}
