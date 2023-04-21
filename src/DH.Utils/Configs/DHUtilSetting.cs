using NewLife.Configuration;

using System.ComponentModel;

using XCode.Configuration;

namespace DH;

/// <summary>工具配置</summary>
[DisplayName("工具配置")]
//[XmlConfigFile("Config/DHUtil.config", 10_000)]
[Config("DHUtilSetting")]
public class DHUtilSetting : Config<DHUtilSetting>
{
    #region 静态
    static DHUtilSetting() => Provider = new DbConfigProvider { UserId = 0, Category = "DH" };
    #endregion

    /// <summary>
    /// Cache键前缀
    /// </summary>
    [Description("Cache键前缀")]
    public String CacheKeyPrefix { get; set; } = "dh";

    #region Redis

    /// <summary>
    /// 是否应该使用Redis服务
    /// </summary>
    [Description("是否应该使用Redis服务")]
    public bool RedisEnabled { get; set; }

    /// <summary>
    /// 获取或设置Redis连接字符串。 启用Redis时使用
    /// </summary>
    [Description("Redis连接字符串")]
    public String RedisConnectionString { get; set; } = "127.0.0.1:6379";

    /// <summary>
    /// 获取或设置Redis连接密码
    /// </summary>
    [Description("Redis连接密码")]
    public String RedisPassWord { get; set; }

    /// <summary>
    /// 获取或设置特定的Redis数据库； 如果需要使用特定的Redis数据库，只需在此处设置其编号。 如果应该为每种数据类型使用不同的数据库，则设置NULL（默认使用）
    /// </summary>
    [Description("特定的Redis数据库")]
    public Int32 RedisDatabaseId { get; set; } = 2;

    #endregion

    #region 方法

    /// <summary>实例化</summary>
    public DHUtilSetting() { }

    #endregion

}
