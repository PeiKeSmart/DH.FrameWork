using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Core.Domain.Customers;

/// <summary>外部身份验证设置</summary>
[DisplayName("外部身份验证设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("ExternalAuthenticationSettings")]
public class ExternalAuthenticationSettings : Config<ExternalAuthenticationSettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static ExternalAuthenticationSettings() => Provider = new DbConfigProvider { UserId = 0, Category = "ExternalAuthentication" };
    #endregion

    /// <summary>
    /// 获取或设置一个值，该值指示是否需要电子邮件验证。
    /// 在大多数情况下，我们可以跳过Facebook或任何其他第三方外部认证插件的电子邮件验证。我想我们可以信任Facebook进行验证。
    /// </summary>
    public bool RequireEmailValidation { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否需要记录身份验证过程中的错误
    /// </summary>
    public bool LogErrors { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否允许用户删除外部身份验证关联
    /// </summary>
    public bool AllowCustomersToRemoveAssociations { get; set; }

    /// <summary>
    /// 获取或设置活动身份验证方法的系统名称
    /// </summary>
    public List<string> ActiveAuthenticationMethodSystemNames { get; set; }
}
