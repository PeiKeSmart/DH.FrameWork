﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using NewLife.Log;
using NewLife.Security;

namespace NewLife.Caching;

/// <summary>缓存</summary>
public abstract class Cache : DisposeBase, ICache
{
    #region 静态默认实现
    /// <summary>默认缓存</summary>
    public static ICache Default { get; set; } = new MemoryCache();
    #endregion

    #region 属性
    /// <summary>名称</summary>
    public String Name { get; set; }

    /// <summary>默认过期时间。避免Set操作时没有设置过期时间，默认0秒表示不过期</summary>
    public Int32 Expire { get; set; }

    /// <summary>获取和设置缓存，使用默认过期时间</summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public virtual Object? this[String key] { get => Get<Object>(key); set => Set(key, value); }

    /// <summary>缓存个数</summary>
    public abstract Int32 Count { get; }

    /// <summary>所有键</summary>
    public abstract ICollection<String> Keys { get; }
    #endregion

    #region 构造
    /// <summary>构造函数</summary>
    protected Cache() => Name = GetType().Name.TrimEnd("Cache");
    #endregion

    #region 基础操作
    /// <summary>使用连接字符串初始化配置</summary>
    /// <param name="config"></param>
    public virtual void Init(String config) { }

    /// <summary>是否包含缓存项</summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public abstract Boolean ContainsKey(String key);

    /// <summary>设置缓存项</summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="expire">过期时间，秒</param>
    /// <returns></returns>
    public abstract Boolean Set<T>(String key, T value, Int32 expire = -1);

    /// <summary>设置缓存项</summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="expire">过期时间</param>
    /// <returns></returns>
    public virtual Boolean Set<T>(String key, T value, TimeSpan expire) => Set(key, value, (Int32)expire.TotalSeconds);

    /// <summary>获取缓存项</summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    [return: MaybeNull]
    public abstract T Get<T>(String key);

    /// <summary>批量移除缓存项</summary>
    /// <param name="keys">键集合</param>
    /// <returns></returns>
    public abstract Int32 Remove(params String[] keys);

    /// <summary>清空所有缓存项</summary>
    public virtual void Clear() => throw new NotSupportedException();

    /// <summary>设置缓存项有效期</summary>
    /// <param name="key">键</param>
    /// <param name="expire">过期时间，秒</param>
    public abstract Boolean SetExpire(String key, TimeSpan expire);

    /// <summary>获取缓存项有效期</summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    public abstract TimeSpan GetExpire(String key);
    #endregion

    #region 集合操作
    /// <summary>批量获取缓存项</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="keys"></param>
    /// <returns></returns>
    public virtual IDictionary<String, T?> GetAll<T>(IEnumerable<String> keys)
    {
        var dic = new Dictionary<String, T?>();
        foreach (var key in keys)
        {
            dic[key] = Get<T>(key);
        }

        return dic;
    }

    /// <summary>批量设置缓存项</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="expire">过期时间，秒</param>
    public virtual void SetAll<T>(IDictionary<String, T> values, Int32 expire = -1)
    {
        foreach (var item in values)
        {
            Set(item.Key, item.Value, expire);
        }
    }

    /// <summary>获取列表</summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="key">键</param>
    /// <returns></returns>
    public virtual IList<T> GetList<T>(String key) => throw new NotSupportedException();

    /// <summary>获取哈希</summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="key">键</param>
    /// <returns></returns>
    public virtual IDictionary<String, T> GetDictionary<T>(String key) => throw new NotSupportedException();

    /// <summary>获取队列</summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="key">键</param>
    /// <returns></returns>
    public virtual IProducerConsumer<T> GetQueue<T>(String key) => throw new NotSupportedException();

    /// <summary>获取栈</summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="key">键</param>
    /// <returns></returns>
    public virtual IProducerConsumer<T> GetStack<T>(String key) => throw new NotSupportedException();

    /// <summary>获取Set</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public virtual ICollection<T> GetSet<T>(String key) => throw new NotSupportedException();
    #endregion

    #region 高级操作
    /// <summary>添加，已存在时不更新</summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="expire">过期时间，秒</param>
    /// <returns></returns>
    public virtual Boolean Add<T>(String key, T value, Int32 expire = -1)
    {
        if (ContainsKey(key)) return false;

        return Set(key, value, expire);
    }

    /// <summary>设置新值并获取旧值，原子操作</summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    [return: MaybeNull]
    public virtual T Replace<T>(String key, T value)
    {
        var rs = Get<T>(key);
        Set(key, value);
        return rs;
    }

    /// <summary>尝试获取指定键，返回是否包含值。有可能缓存项刚好是默认值，或者只是反序列化失败</summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值。即使有值也不一定能够返回，可能缓存项刚好是默认值，或者只是反序列化失败</param>
    /// <returns>返回是否包含值，即使反序列化失败</returns>
    public virtual Boolean TryGetValue<T>(String key, [MaybeNull] out T value)
    {
        value = Get<T>(key);
        if (!Equals(value, default)) return true;

        return ContainsKey(key);
    }

    /// <summary>获取 或 添加 缓存数据，在数据不存在时执行委托请求数据</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    /// <param name="expire">过期时间，秒。小于0时采用默认缓存时间<seealso cref="Cache.Expire"/></param>
    /// <returns></returns>
    [return: MaybeNull]
    public virtual T GetOrAdd<T>(String key, Func<String, T> callback, Int32 expire = -1)
    {
        var value = Get<T>(key);
        if (!Equals(value, default)) return value;

        if (ContainsKey(key)) return value;

        value = callback(key);

        if (expire < 0) expire = Expire;
        if (Add(key, value, expire)) return value;

        return Get<T>(key);
    }

    /// <summary>累加，原子操作</summary>
    /// <param name="key">键</param>
    /// <param name="value">变化量</param>
    /// <returns></returns>
    public virtual Int64 Increment(String key, Int64 value)
    {
        lock (this)
        {
            var v = Get<Int64>(key);
            v += value;
            Set(key, v);

            return v;
        }
    }

    /// <summary>累加，原子操作</summary>
    /// <param name="key">键</param>
    /// <param name="value">变化量</param>
    /// <returns></returns>
    public virtual Double Increment(String key, Double value)
    {
        lock (this)
        {
            var v = Get<Double>(key);
            v += value;
            Set(key, v);

            return v;
        }
    }

    /// <summary>递减，原子操作</summary>
    /// <param name="key">键</param>
    /// <param name="value">变化量</param>
    /// <returns></returns>
    public virtual Int64 Decrement(String key, Int64 value)
    {
        lock (this)
        {
            var v = Get<Int64>(key);
            v -= value;
            Set(key, v);

            return v;
        }
    }

    /// <summary>递减，原子操作</summary>
    /// <param name="key">键</param>
    /// <param name="value">变化量</param>
    /// <returns></returns>
    public virtual Double Decrement(String key, Double value)
    {
        lock (this)
        {
            var v = Get<Double>(key);
            v -= value;
            Set(key, v);

            return v;
        }
    }
    #endregion

    #region 事务
    /// <summary>提交变更。部分提供者需要刷盘</summary>
    /// <returns></returns>
    public virtual Int32 Commit() => 0;

    /// <summary>申请分布式锁</summary>
    /// <param name="key">要锁定的key</param>
    /// <param name="msTimeout">锁等待时间，单位毫秒</param>
    /// <returns></returns>
    public IDisposable? AcquireLock(String key, Int32 msTimeout)
    {
        var rlock = new CacheLock(this, key);
        if (!rlock.Acquire(msTimeout, msTimeout)) throw new InvalidOperationException($"Lock [{key}] failed! msTimeout={msTimeout}");

        return rlock;
    }

    /// <summary>申请分布式锁</summary>
    /// <param name="key">要锁定的key</param>
    /// <param name="msTimeout">锁等待时间，申请加锁时如果遇到冲突则等待的最大时间，单位毫秒</param>
    /// <param name="msExpire">锁过期时间，超过该时间如果没有主动释放则自动释放锁，必须整数秒，单位毫秒</param>
    /// <param name="throwOnFailure">失败时是否抛出异常，如果不抛出异常，可通过返回null得知申请锁失败</param>
    /// <returns></returns>
    public IDisposable? AcquireLock(String key, Int32 msTimeout, Int32 msExpire, Boolean throwOnFailure)
    {
        var rlock = new CacheLock(this, key);
        if (!rlock.Acquire(msTimeout, msExpire))
        {
            if (throwOnFailure) throw new InvalidOperationException($"Lock [{key}] failed! msTimeout={msTimeout}");

            return null;
        }

        return rlock;
    }
    #endregion

    #region 性能测试
    /// <summary>多线程性能测试</summary>
    /// <param name="rand">随机读写。顺序，每个线程多次操作一个key；随机，每个线程每次操作不同key</param>
    /// <param name="batch">批量操作。默认0不分批，分批仅针对随机读写，对顺序读写的单key操作没有意义</param>
    /// <remarks>
    /// Memory性能测试[顺序]，逻辑处理器 32 个 2,000MHz Intel(R) Xeon(R) CPU E5-2640 v2 @ 2.00GHz
    /// 
    /// 测试 10,000,000 项，  1 线程
    /// 赋值 10,000,000 项，  1 线程，耗时   3,764ms 速度 2,656,748 ops
    /// 读取 10,000,000 项，  1 线程，耗时   1,296ms 速度 7,716,049 ops
    /// 删除 10,000,000 项，  1 线程，耗时   1,230ms 速度 8,130,081 ops
    /// 
    /// 测试 20,000,000 项，  2 线程
    /// 赋值 20,000,000 项，  2 线程，耗时   3,088ms 速度 6,476,683 ops
    /// 读取 20,000,000 项，  2 线程，耗时   1,051ms 速度 19,029,495 ops
    /// 删除 20,000,000 项，  2 线程，耗时   1,011ms 速度 19,782,393 ops
    /// 
    /// 测试 40,000,000 项，  4 线程
    /// 赋值 40,000,000 项，  4 线程，耗时   3,060ms 速度 13,071,895 ops
    /// 读取 40,000,000 项，  4 线程，耗时   1,023ms 速度 39,100,684 ops
    /// 删除 40,000,000 项，  4 线程，耗时     994ms 速度 40,241,448 ops
    /// 
    /// 测试 80,000,000 项，  8 线程
    /// 赋值 80,000,000 项，  8 线程，耗时   3,124ms 速度 25,608,194 ops
    /// 读取 80,000,000 项，  8 线程，耗时   1,171ms 速度 68,317,677 ops
    /// 删除 80,000,000 项，  8 线程，耗时   1,199ms 速度 66,722,268 ops
    /// 
    /// 测试 320,000,000 项， 32 线程
    /// 赋值 320,000,000 项， 32 线程，耗时  13,857ms 速度 23,093,021 ops
    /// 读取 320,000,000 项， 32 线程，耗时   1,950ms 速度 164,102,564 ops
    /// 删除 320,000,000 项， 32 线程，耗时   3,359ms 速度 95,266,448 ops
    /// 
    /// 测试 320,000,000 项， 64 线程
    /// 赋值 320,000,000 项， 64 线程，耗时   9,648ms 速度 33,167,495 ops
    /// 读取 320,000,000 项， 64 线程，耗时   1,974ms 速度 162,107,396 ops
    /// 删除 320,000,000 项， 64 线程，耗时   1,907ms 速度 167,802,831 ops
    /// 
    /// 测试 320,000,000 项，256 线程
    /// 赋值 320,000,000 项，256 线程，耗时  12,429ms 速度 25,746,238 ops
    /// 读取 320,000,000 项，256 线程，耗时   1,907ms 速度 167,802,831 ops
    /// 删除 320,000,000 项，256 线程，耗时   2,350ms 速度 136,170,212 ops
    /// </remarks>
    public virtual Int64 Bench(Boolean rand = false, Int32 batch = 0)
    {
        var cpu = Environment.ProcessorCount;
        XTrace.WriteLine($"{Name}性能测试[{(rand ? "随机" : "顺序")}]，批大小[{batch}]，逻辑处理器 {cpu:n0} 个");

        var rs = 0L;
        var times = 10_000;

        // 单线程
        rs += BenchOne(times, 1, rand, batch);

        // 多线程
        if (cpu != 2) rs += BenchOne(times * 2, 2, rand, batch);
        if (cpu != 4) rs += BenchOne(times * 4, 4, rand, batch);
        if (cpu != 8) rs += BenchOne(times * 8, 8, rand, batch);

        // CPU个数
        rs += BenchOne(times * cpu, cpu, rand, batch);

        //// 1.5倍
        //var cpu2 = cpu * 3 / 2;
        //if (!(new[] { 2, 4, 8, 64, 256 }).Contains(cpu2)) BenchOne(times * cpu2, cpu2, rand);

        // 最大
        if (cpu < 64) rs += BenchOne(times * cpu, 64, rand, batch);
        //if (cpu * 8 >= 256) BenchOne(times * cpu, cpu * 8, rand);

        return rs;
    }

    /// <summary>使用指定线程测试指定次数</summary>
    /// <param name="times">次数</param>
    /// <param name="threads">线程</param>
    /// <param name="rand">随机读写</param>
    /// <param name="batch">批量操作</param>
    public virtual Int64 BenchOne(Int64 times, Int32 threads, Boolean rand, Int32 batch)
    {
        if (threads <= 0) threads = Environment.ProcessorCount;
        if (times <= 0) times = threads * 1_000;

        //XTrace.WriteLine("");
        XTrace.WriteLine($"测试 {times:n0} 项，{threads,3:n0} 线程");

        var rs = 3L;

        //提前执行一次网络操作，预热链路
        var key = "bstr_";
        Set(key, Rand.NextString(32));
        _ = Get<String>(key);
        Remove(key);

        // 赋值测试
        rs += BenchSet(key, times, threads, rand, batch);

        // 读取测试
        rs += BenchGet(key, times, threads, rand, batch);

        // 删除测试
        rs += BenchRemove(key, times, threads, rand, batch);

        // 累加测试
        key = "bint_";
        rs += BenchInc(key, times, threads, rand, batch);

        return rs;
    }

    /// <summary>读取测试</summary>
    /// <param name="key">键</param>
    /// <param name="times">次数</param>
    /// <param name="threads">线程</param>
    /// <param name="rand">随机读写</param>
    /// <param name="batch">批量操作</param>
    protected virtual Int64 BenchGet(String key, Int64 times, Int32 threads, Boolean rand, Int32 batch)
    {
        //提前执行一次网络操作，预热链路
        var v = Get<String>(key);

        var sw = Stopwatch.StartNew();
        if (rand)
        {
            // 随机操作，每个线程每次操作不同key，跳跃式
            Parallel.For(0, threads, k =>
            {
                if (batch == 0)
                {
                    for (var i = k; i < times; i += threads)
                    {
                        var val = Get<String>(key + i);
                    }
                }
                else
                {
                    var n = 0;
                    var keys = new String[batch];
                    for (var i = k; i < times; i += threads)
                    {
                        keys[n++] = key + i;

                        if (n >= batch)
                        {
                            var vals = GetAll<String>(keys);
                            n = 0;
                        }
                    }
                    if (n > 0)
                    {
                        var vals = GetAll<String>(keys.Take(n));
                    }
                }
            });
        }
        else
        {
            // 顺序操作，每个线程多次操作同一个key
            Parallel.For(0, threads, k =>
            {
                var mykey = key + k;
                var count = times / threads;
                for (var i = 0; i < count; i++)
                {
                    var val = Get<String>(mykey);
                }
            });
        }
        sw.Stop();

        var speed = times * 1000 / sw.ElapsedMilliseconds;
        XTrace.WriteLine($"读取 耗时 {sw.ElapsedMilliseconds,7:n0}ms 速度 {speed,9:n0} ops");

        return times + 1;
    }

    /// <summary>赋值测试</summary>
    /// <param name="key">键</param>
    /// <param name="times">次数</param>
    /// <param name="threads">线程</param>
    /// <param name="rand">随机读写</param>
    /// <param name="batch">批量操作</param>
    protected virtual Int64 BenchSet(String key, Int64 times, Int32 threads, Boolean rand, Int32 batch)
    {
        Set(key, Rand.NextString(32));

        var sw = Stopwatch.StartNew();
        if (rand)
        {
            // 随机操作，每个线程每次操作不同key，跳跃式
            Parallel.For(0, threads, k =>
            {
                var val = Rand.NextString(8);
                if (batch == 0)
                {
                    for (var i = k; i < times; i += threads)
                    {
                        Set(key + i, val);
                    }
                }
                else
                {
                    var n = 0;
                    var dic = new Dictionary<String, String>();
                    for (var i = k; i < times; i += threads)
                    {
                        dic[key + i] = val;
                        n++;

                        if (n >= batch)
                        {
                            SetAll(dic);
                            dic.Clear();
                            n = 0;
                        }
                    }
                    if (n > 0)
                    {
                        SetAll(dic);
                    }
                }

                // 提交变更
                Commit();
            });
        }
        else
        {
            // 顺序操作，每个线程多次操作同一个key
            Parallel.For(0, threads, k =>
            {
                var mykey = key + k;
                var val = Rand.NextString(8);
                var count = times / threads;
                for (var i = 0; i < count; i++)
                {
                    Set(mykey, val);
                }

                // 提交变更
                Commit();
            });
        }
        sw.Stop();

        var speed = times * 1000 / sw.ElapsedMilliseconds;
        XTrace.WriteLine($"赋值 耗时 {sw.ElapsedMilliseconds,7:n0}ms 速度 {speed,9:n0} ops");

        return times + 1;
    }

    /// <summary>删除测试</summary>
    /// <param name="key">键</param>
    /// <param name="times">次数</param>
    /// <param name="threads">线程</param>
    /// <param name="rand">随机读写</param>
    /// <param name="batch">批量操作</param>
    protected virtual Int64 BenchRemove(String key, Int64 times, Int32 threads, Boolean rand, Int32 batch)
    {
        //提前执行一次网络操作，预热链路
        Remove(key);

        var sw = Stopwatch.StartNew();
        if (rand)
        {
            // 随机操作，每个线程每次操作不同key，跳跃式
            Parallel.For(0, threads, k =>
            {
                if (batch == 0)
                {
                    for (var i = k; i < times; i += threads)
                    {
                        Remove(key + i);
                    }
                }
                else
                {
                    var n = 0;
                    var keys = new String[batch];
                    for (var i = k; i < times; i += threads)
                    {
                        keys[n++] = key + i;

                        if (n >= batch)
                        {
                            Remove(keys);
                            n = 0;
                        }
                    }
                    if (n > 0)
                    {
                        Remove(keys.Take(n).ToArray());
                    }
                }

                // 提交变更
                Commit();
            });
        }
        else
        {
            // 顺序操作，每个线程多次操作同一个key
            Parallel.For(0, threads, k =>
            {
                var mykey = key + k;
                var count = times / threads;
                for (var i = 0; i < count; i++)
                {
                    Remove(mykey);
                }

                // 提交变更
                Commit();
            });
        }
        sw.Stop();

        var speed = times * 1000 / sw.ElapsedMilliseconds;
        XTrace.WriteLine($"删除 耗时 {sw.ElapsedMilliseconds,7:n0}ms 速度 {speed,9:n0} ops");

        return times + 1;
    }

    /// <summary>累加测试</summary>
    /// <param name="key">键</param>
    /// <param name="times">次数</param>
    /// <param name="threads">线程</param>
    /// <param name="rand">随机读写</param>
    /// <param name="batch">批量操作</param>
    protected virtual Int64 BenchInc(String key, Int64 times, Int32 threads, Boolean rand, Int32 batch)
    {
        //提前执行一次网络操作，预热链路
        Increment(key, 1);

        var sw = Stopwatch.StartNew();
        if (rand)
        {
            // 随机操作，每个线程每次操作不同key，跳跃式
            Parallel.For(0, threads, k =>
            {
                var val = Rand.Next(100);
                for (var i = k; i < times; i += threads)
                {
                    Increment(key + i, val);
                }

                // 提交变更
                Commit();
            });
        }
        else
        {
            // 顺序操作，每个线程多次操作同一个key
            Parallel.For(0, threads, k =>
            {
                var mykey = key + k;
                var val = Rand.Next(100);
                var count = times / threads;
                for (var i = 0; i < count; i++)
                {
                    Increment(mykey, val);
                }

                // 提交变更
                Commit();
            });
        }
        sw.Stop();

        var speed = times * 1000 / sw.ElapsedMilliseconds;
        XTrace.WriteLine($"累加 耗时 {sw.ElapsedMilliseconds,7:n0}ms 速度 {speed,9:n0} ops");

        return times + 1;
    }
    #endregion

    #region 辅助
    /// <summary>已重载。</summary>
    /// <returns></returns>
    public override String ToString() => Name;
    #endregion
}