using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Core.Domain.Security;

/// <summary>安全设置</summary>
[DisplayName("安全设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("SecuritySettings")]
public class SecuritySettings : Config<SecuritySettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static SecuritySettings() => Provider = new DbConfigProvider { UserId = 0, Category = "Security" };
    #endregion

    /// <summary>
    /// 获取或设置加密密钥
    /// </summary>
    public string EncryptionKey { get; set; }

    /// <summary>
    /// 获取或设置管理区域允许的IP地址列表
    /// </summary>
    public List<string> AdminAreaAllowedIpAddresses { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否在注册页上启用蜜罐
    /// </summary>
    public bool HoneypotEnabled { get; set; }

    /// <summary>
    /// 获取或设置蜜罐输入名称
    /// </summary>
    public string HoneypotInputName { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示头中是否允许非ASCII字符
    /// </summary>
    public bool AllowNonAsciiCharactersInHeaders { get; set; }
}
