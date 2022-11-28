using NewLife.Xml;

using System.ComponentModel;

namespace DH;

/// <summary>工具配置</summary>
[DisplayName("工具配置")]
[XmlConfigFile("Config/Util.config", 10_000)]
public class UtilSetting : XmlConfig<UtilSetting>
{
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

    #endregion

    #region 方法

    /// <summary>实例化</summary>
    public UtilSetting() { }

    #endregion

}
