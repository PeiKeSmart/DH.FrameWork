using NewLife.Caching;
using NewLife.Log;

using Pek.Configs;
using Pek.Infrastructure;

namespace DH.Permissions.Identity.JwtBearer.Internal;

/// <summary>
/// 令牌Payload存储器
/// </summary>
internal sealed class TokenPayloadStore : ITokenPayloadStore
{
    /// <summary>
    /// 缓存
    /// </summary>
    private readonly ICache _cache;

    /// <summary>
    /// 初始化一个<see cref="TokenPayloadStore"/>类型的实例
    /// </summary>
    /// <param name="cache"></param>
    public TokenPayloadStore(ICache cache)
    {
        if (RedisSetting.Current.RedisEnabled)
        {
            _cache = Singleton<FullRedis>.Instance;
            if (_cache == null)
            {
                XTrace.WriteException(new Exception($"Redis缓存对象为空，请检查是否注入FullRedis"));
            }
        }
        else
        {
            _cache = cache;
        }
        //if (DHUtilSetting.Current.IsUseRedisCache)
        //{
        //    _cache = EngineContext.Current.Resolve<ICache>();
        //}
        //else
        //{
        //    _cache = cache;
        //}
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="token">令牌</param>
    /// <param name="payload">负载字典</param>
    /// <param name="expires">过期时间</param>
    public void Save(string token, IDictionary<string, string> payload, DateTime expires) =>
        _cache.Set(GetPayloadKey(token), payload, expires.Subtract(DateTime.UtcNow));

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="token">令牌</param>
    public void Remove(string token)
    {
        if (!_cache.ContainsKey(GetPayloadKey(token)))
            return;
        _cache.Remove(GetPayloadKey(token));
    }

    /// <summary>
    /// 延时移除
    /// </summary>
    /// <param name="token">令牌</param>
    /// <param name="expire">延时时间。秒</param>
    public void Remove(string token, Int32 expire)
    {
        var key = GetPayloadKey(token);

        if (!_cache.ContainsKey(key))
            return;

        _cache.SetExpire(key, TimeSpan.FromSeconds(expire));
    }

    /// <summary>
    /// 获取Payload
    /// </summary>
    /// <param name="token">令牌</param>
    public IDictionary<string, string> Get(string token) =>
        _cache.Get<IDictionary<string, string>>(GetPayloadKey(token));

    /// <summary>
    /// 获取Payload缓存键
    /// </summary>
    /// <param name="token">令牌</param>
    private static string GetPayloadKey(string token) => $"jwt:token:payload:{token}";
}
