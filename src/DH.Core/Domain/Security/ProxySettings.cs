using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Core.Domain.Security;

/// <summary>代理设置</summary>
[DisplayName("代理设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("ProxySettings")]
public class ProxySettings : Config<ProxySettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static ProxySettings() => Provider = new DbConfigProvider { UserId = 0, Category = "Proxy" };
    #endregion

    /// <summary>
    /// 获取或设置一个值，该值指示是否应使用代理连接
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 获取或设置代理服务器的地址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 获取或设置代理服务器的端口
    /// </summary>
    public string Port { get; set; }

    /// <summary>
    /// 获取或设置代理连接的用户名
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 获取或设置代理连接的密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否为本地地址绕过代理服务器
    /// </summary>
    public bool BypassOnLocal { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示处理程序是否随请求发送Authorization标头
    /// </summary>
    public bool PreAuthenticate { get; set; }
}
