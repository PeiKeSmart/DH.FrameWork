using NewLife.Caching;
using NewLife.Log;

using Pek.Configs;
using Pek.Infrastructure;
using Pek.Security;

namespace DH.Permissions.Identity.JwtBearer.Internal;

/// <summary>
/// Jwt令牌存储器
/// </summary>
internal sealed class JsonWebTokenStore : IJsonWebTokenStore
{
    /// <summary>
    /// 缓存
    /// </summary>
    private readonly ICache _cache;

    /// <summary>
    /// 初始化一个<see cref="JsonWebTokenStore"/>类型的实例
    /// </summary>
    /// <param name="cache">缓存</param>
    public JsonWebTokenStore(ICache cache)
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
    /// 获取刷新令牌
    /// </summary>
    /// <param name="token">刷新令牌</param>
    public RefreshToken GetRefreshToken(string token) =>
        _cache.Get<RefreshToken>(GetRefreshTokenKey(token));

    /// <summary>
    /// 保存刷新令牌
    /// </summary>
    /// <param name="token">刷新令牌</param>
    public void SaveRefreshToken(RefreshToken token) => _cache.Set(GetRefreshTokenKey(token.Value), token, token.EndUtcTime.Subtract(DateTime.UtcNow));

    /// <summary>
    /// 移除刷新令牌
    /// </summary>
    /// <param name="token">刷新令牌</param>
    public void RemoveRefreshToken(string token)
    {
        if (!_cache.ContainsKey(GetRefreshTokenKey(token)))
            return;
        _cache.Remove(GetRefreshTokenKey(token));
        if (!_cache.ContainsKey(GetBindRefreshTokenKey(token)))
            return;
        var accessToken = _cache.Get<JsonWebToken>(GetBindRefreshTokenKey(token));
        _cache.Remove(GetBindRefreshTokenKey(token));
        RemoveToken(accessToken.AccessToken);
    }

    /// <summary>
    /// 移除刷新令牌
    /// </summary>
    /// <param name="token">刷新令牌</param>
    /// <param name="expire">延时时间。秒</param>
    public void RemoveRefreshToken(string token, Int32 expire)
    {
        var key = GetRefreshTokenKey(token);
        var key1 = GetBindRefreshTokenKey(token);

        if (!_cache.ContainsKey(key))
            return;
        _cache.SetExpire(key, TimeSpan.FromSeconds(expire));

        if (!_cache.ContainsKey(key1))
            return;
        _cache.SetExpire(key1, TimeSpan.FromSeconds(expire));

        var accessToken = _cache.Get<JsonWebToken>(key1);
        RemoveToken(accessToken.AccessToken, expire);
    }

    /// <summary>
    /// 获取访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    public JsonWebToken GetToken(string token) => _cache.Get<JsonWebToken>(GetTokenKey(token));

    /// <summary>
    /// 移除访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    public void RemoveToken(string token)
    {
        if (!_cache.ContainsKey(GetTokenKey(token)))
            return;
        _cache.Remove(GetTokenKey(token));
    }

    /// <summary>
    /// 移除访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    public void RemoveToken(string token, Int32 expire)
    {
        var key = GetTokenKey(token);

        if (!_cache.ContainsKey(key))
            return;

        _cache.SetExpire(key, TimeSpan.FromSeconds(expire));
    }

    /// <summary>
    /// 保存访问令牌
    /// </summary>
    /// <param name="token">令牌</param>
    /// <param name="expires">过期时间</param>
    public void SaveToken(JsonWebToken token, DateTime expires)
    {
        _cache.Set(GetTokenKey(token.AccessToken), token, expires.Subtract(DateTime.UtcNow));
        _cache.Set(GetBindRefreshTokenKey(token.RefreshToken), token, expires.Subtract(DateTime.UtcNow));
    }

    /// <summary>
    /// 是否存在访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    public bool ExistsToken(string token) => _cache.ContainsKey(GetTokenKey(token));

    /// <summary>
    /// 绑定用户设备令牌
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="clientType">客户端类型</param>
    /// <param name="info">设备信息</param>
    /// <param name="expires">过期时间</param>
    public void BindUserDeviceToken(string userId, string clientType, DeviceTokenBindInfo info,
        DateTime expires) => _cache.Set(GetBindUserDeviceTokenKey(userId, clientType), info,
        expires.Subtract(DateTime.UtcNow));

    /// <summary>
    /// 获取用户设备令牌
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="clientType">客户端类型</param>
    public DeviceTokenBindInfo GetUserDeviceToken(string userId, string clientType) =>
        _cache.Get<DeviceTokenBindInfo>(GetBindUserDeviceTokenKey(userId, clientType));

    /// <summary>
    /// 获取刷新令牌缓存键
    /// </summary>
    /// <param name="token">刷新令牌</param>
    private static string GetRefreshTokenKey(string token) => $"jwt:token:refresh:{token}";

    /// <summary>
    /// 获取访问令牌缓存键
    /// </summary>
    /// <param name="token">访问令牌</param>
    private static string GetTokenKey(string token) => $"jwt:token:access:{token}";

    /// <summary>
    /// 获取绑定刷新令牌缓存键
    /// </summary>
    /// <param name="token">刷新令牌</param>
    private static string GetBindRefreshTokenKey(string token) => $"jwt:token:bind:{token}";

    /// <summary>
    /// 获取绑定用户设备令牌缓存键
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="clientType">客户端类型</param>
    private static string GetBindUserDeviceTokenKey(string userId, string clientType) =>
        $"jwt:token:bind_user:{userId}:{clientType}";
}
