﻿using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewLife.Caching;
using NewLife.Log;

namespace NewLife.Redis.Extensions;

/// <summary>
/// Redis分布式缓存
/// </summary>
public class RedisCache : IDistributedCache, IDisposable
{
    #region 属性
    /// <summary>
    /// Redis对象。可使用完整Redis功能
    /// </summary>
    public FullRedis Redis => _redis;

    /// <summary>刷新时的过期时间。默认24小时</summary>
    public TimeSpan Expire { get; set; } = TimeSpan.FromHours(24);

    private readonly RedisOptions _options;
    private readonly FullRedis _redis;
    #endregion

    #region 构造
    /// <summary>
    /// 实例化Redis分布式缓存
    /// </summary>
    /// <param name="optionsAccessor"></param>
    /// <param name="serviceProvider"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public RedisCache(IOptions<RedisOptions> optionsAccessor, IServiceProvider serviceProvider)
    {
        if (optionsAccessor == null) throw new ArgumentNullException(nameof(optionsAccessor));

        _options = optionsAccessor.Value;

        //_redis = new FullRedis
        //{
        //    Name = _options.InstanceName,
        //    Tracer = serviceProvider.GetService<ITracer>(),
        //};
        _redis = _options.Prefix.IsNullOrEmpty() ? new FullRedis() : new PrefixedRedis();
        _redis.Name = _options.InstanceName;
        _redis.Tracer = serviceProvider.GetService<ITracer>();

        if (!_options.Configuration.IsNullOrEmpty())
            _redis.Init(_options.Configuration);
        else
        {
            _redis.Server = _options.Server;
            _redis.Db = _options.Db;
            _redis.Password = _options.Password;
            _redis.Timeout = _options.Timeout;
        }
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Dispose() => _redis?.Dispose();
    #endregion

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public Byte[]? Get(String key) => _redis.Get<Byte[]>(key);

    /// <summary>
    /// 异步获取
    /// </summary>
    /// <param name="key"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Byte[]?> GetAsync(String key, CancellationToken token = default) => Task.Run(() => _redis.Get<Byte[]>(key), token);

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Set(String key, Byte[] value, DistributedCacheEntryOptions options)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));

        if (options == null)
            _redis.Set(key, value);
        else
            if (options.AbsoluteExpiration != null)
            _redis.Set(key, value, options.AbsoluteExpiration.Value - DateTime.Now);
        else if (options.AbsoluteExpirationRelativeToNow != null)
            _redis.Set(key, value, options.AbsoluteExpirationRelativeToNow.Value);
        else if (options.SlidingExpiration != null)
            _redis.Set(key, value, options.SlidingExpiration.Value);
        else
            _redis.Set(key, value);
    }

    /// <summary>
    /// 异步设置
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task SetAsync(String key, Byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default) => Task.Run(() => Set(key, value, options), token);

    /// <summary>
    /// 刷新
    /// </summary>
    /// <param name="key"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Refresh(String key) => _redis.SetExpire(key, Expire);

    /// <summary>
    /// 异步刷新
    /// </summary>
    /// <param name="key"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Task RefreshAsync(String key, CancellationToken token = default) => Task.Run(() => Refresh(key), token);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="key"></param>
    public void Remove(String key) => _redis.Remove(key);

    /// <summary>
    /// 异步删除
    /// </summary>
    /// <param name="key"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task RemoveAsync(String key, CancellationToken token = default) => Task.Run(() => _redis.Remove(key), token);
}