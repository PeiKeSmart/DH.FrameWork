using Pek.Security;

namespace DH.Permissions.Identity.JwtBearer;

/// <summary>
/// Jwt令牌存储器
/// </summary>
public interface IJsonWebTokenStore
{
    /// <summary>
    /// 获取刷新令牌
    /// </summary>
    /// <param name="token">刷新令牌</param>
    RefreshToken GetRefreshToken(string token);

    /// <summary>
    /// 保存刷新令牌
    /// </summary>
    /// <param name="token">刷新令牌</param>
    void SaveRefreshToken(RefreshToken token);

    /// <summary>
    /// 移除刷新令牌
    /// </summary>
    /// <param name="token">刷新令牌</param>
    void RemoveRefreshToken(string token);

    /// <summary>
    /// 延时移除刷新令牌
    /// </summary>
    /// <param name="token">刷新令牌</param>
    /// <param name="expire">延时时间。秒</param>
    void RemoveRefreshToken(string token, Int32 expire);

    /// <summary>
    /// 获取访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    JsonWebToken GetToken(string token);

    /// <summary>
    /// 移除访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    void RemoveToken(string token);

    /// <summary>
    /// 延时移除访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    /// <param name="expire">延时时间。秒</param>
    void RemoveToken(string token, Int32 expire);

    /// <summary>
    /// 保存访问令牌
    /// </summary>
    /// <param name="token">令牌</param>
    /// <param name="expires">过期时间</param>
    void SaveToken(JsonWebToken token, DateTime expires);

    /// <summary>
    /// 是否存在访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    bool ExistsToken(string token);

    /// <summary>
    /// 绑定用户设备令牌
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="clientType">客户端类型</param>
    /// <param name="info">设备信息</param>
    /// <param name="expires">过期时间</param>
    void BindUserDeviceToken(string userId, string clientType, DeviceTokenBindInfo info, DateTime expires);

    /// <summary>
    /// 获取用户设备令牌
    /// </summary>
    /// <param name="userId">用户标识</param>
    /// <param name="clientType">客户端类型</param>
    DeviceTokenBindInfo GetUserDeviceToken(string userId, string clientType);
}
