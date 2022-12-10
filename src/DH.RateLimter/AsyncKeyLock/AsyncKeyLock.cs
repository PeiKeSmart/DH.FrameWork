using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DH.RateLimter
{
    /// <summary>
    /// 异步密钥锁可防止多个异步线程同时使用给定密钥对同一个对象进行操作。
    /// 它被设计为不会阻塞允许高吞吐量的唯一请求。
    /// </summary>
    internal sealed class AsyncKeyLock
    {
        /// <summary>
        /// 门卫计数器的集合，用于跟踪对同一键的引用。
        /// </summary>
        private static readonly Dictionary<string, AsyncKeyLockDoorman> Keys = new Dictionary<string, AsyncKeyLockDoorman>();

        /// <summary>
        /// 门卫计数器的集合，用于跟踪对同一键的引用。
        /// </summary>
        private static readonly Stack<AsyncKeyLockDoorman> Pool = new Stack<AsyncKeyLockDoorman>(MaxPoolSize);

        /// <summary>
        /// 看门人池的最大大小。 如果释放时池已满
        /// 一个门卫，它只是留给垃圾收集。
        /// </summary>
        private const int MaxPoolSize = 20;

        /// <summary>
        /// SpinLock用于保护对Keys和Pool集合的访问。
        /// </summary>
        private static SpinLock _spinLock = new SpinLock(false);

        /// <summary>
        /// 异步将当前线程锁定为读取模式。
        /// </summary>
        /// <param name="key">标识要锁定的特定对象的键。</param>
        /// <returns>
        /// The <see cref="Task{IDisposable}"/> 这将释放锁。
        /// </returns>
        public async Task<IDisposable> ReaderLockAsync(string key)
        {
            AsyncKeyLockDoorman doorman = GetDoorman(key);

            return await doorman.ReaderLockAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 异步将当前线程锁定为写入模式。
        /// </summary>
        /// <param name="key">标识要锁定的特定对象的键。</param>
        /// <returns>
        /// The <see cref="Task{IDisposable}"/> 这将释放锁。
        /// </returns>
        public async Task<IDisposable> WriterLockAsync(string key)
        {
            AsyncKeyLockDoorman doorman = GetDoorman(key);

            return await doorman.WriterLockAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 获取指定键的门卫。 如果不存在这样的门卫，则未使用的门卫
        /// 是从池中获取的（如果池为空，则分配一个新的），并且
        /// 分配给请求的密钥。
        /// </summary>
        /// <param name="key">所需门卫的钥匙。</param>
        /// <returns>The <see cref="Doorman"/>.</returns>
        private static AsyncKeyLockDoorman GetDoorman(string key)
        {
            AsyncKeyLockDoorman doorman;
            bool lockTaken = false;
            try
            {
                _spinLock.Enter(ref lockTaken);

                if (!Keys.TryGetValue(key, out doorman))
                {
                    doorman = (Pool.Count > 0) ? Pool.Pop() : new AsyncKeyLockDoorman(ReleaseDoorman);
                    doorman.Key = key;
                    Keys.Add(key, doorman);
                }

                doorman.RefCount++;
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit();
                }
            }

            return doorman;
        }

        /// <summary>
        /// Releases a reference to a doorman. If the ref-count hits zero, then the doorman is
        /// returned to the pool (or is simply left for the garbage collector to cleanup if the
        /// pool is already full).
        /// </summary>
        /// <param name="doorman">The <see cref="Doorman"/>.</param>
        private static void ReleaseDoorman(AsyncKeyLockDoorman doorman)
        {
            bool lockTaken = false;
            try
            {
                _spinLock.Enter(ref lockTaken);

                if (--doorman.RefCount == 0)
                {
                    Keys.Remove(doorman.Key);
                    if (Pool.Count < MaxPoolSize)
                    {
                        doorman.Key = null;
                        Pool.Push(doorman);
                    }
                }
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit();
                }
            }
        }
    }
}
