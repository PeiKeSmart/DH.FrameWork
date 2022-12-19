namespace DH.Permissions.Identity.JwtBearer;

/// <summary>
/// 令牌Payload存储器
/// </summary>
public interface ITokenPayloadStore
{
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="token">令牌</param>
    /// <param name="payload">负载字典</param>
    /// <param name="expires">过期时间</param>
    void Save(string token, IDictionary<string, string> payload, DateTime expires);

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="token">令牌</param>
    void Remove(string token);

    /// <summary>
    /// 延时移除
    /// </summary>
    /// <param name="token">令牌</param>
    void Remove(string token, Int32 expire);

    /// <summary>
    /// 获取Payload
    /// </summary>
    /// <param name="token">令牌</param>
    IDictionary<string, string> Get(string token);
}
