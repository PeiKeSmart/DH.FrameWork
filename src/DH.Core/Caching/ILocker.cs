namespace DH.Core.Caching
{
    public interface ILocker
    {
        /// <summary>
        /// 使用内存中的独占锁执行异步操作
        /// </summary>
        /// <param name="resource">我们正在锁定的键</param>
        /// <param name="expirationTime">锁将自动过期的时间</param>
        /// <param name="action">要使用锁定执行的操作</param>
        /// <returns>如果获取了锁并执行了操作，则为True；否则为false</returns>
        Task<bool> PerformActionWithLockAsync(string resource, TimeSpan expirationTime, Func<Task> action);
    }
}
