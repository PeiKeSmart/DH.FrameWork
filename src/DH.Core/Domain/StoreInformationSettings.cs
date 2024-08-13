using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Core.Domain;

/// <summary>站点信息设置</summary>
[DisplayName("站点信息设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("StoreInformationSettings")]
public class StoreInformationSettings : Config<StoreInformationSettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static StoreInformationSettings() => Provider = new DbConfigProvider { UserId = 0, Category = "StoreInformation" };
    #endregion

    /// <summary>
    /// 获取或设置一个值，该值指示是否应显示"powered by YuanRenYi"文本。
    /// </summary>
    public bool HidePoweredByYuanRenYi { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether store is closed
    /// </summary>
    public bool StoreClosed { get; set; }

    /// <summary>
    /// Gets or sets a picture identifier of the logo. If 0, then the default one will be used
    /// </summary>
    public int LogoPictureId { get; set; }

    /// <summary>
    /// 获取或设置默认存储主题
    /// </summary>
    public string DefaultStoreTheme { get; set; } = "DefaultClean";

    /// <summary>
    /// 获取或设置一个值，该值指示是否允许客户选择主题
    /// </summary>
    public Boolean AllowCustomerToSelectTheme { get; set; }

    /// <summary>
    /// 会员级别
    /// </summary>
    public String MemberGrade { get; set; }

    /// <summary>
    /// 是否使用HTTP_CLUSTER_HTTPS
    /// </summary>
    public Boolean UseHttpClusterHttps { get; set; }

    /// <summary>
    /// 是否使用HTTP_X_FORWARDED_PROTO
    /// </summary>
    public Boolean UseHttpXForwardedProto { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether we should display warnings about the new EU cookie law
    /// </summary>
    public bool DisplayEuCookieLawWarning { get; set; }

    /// <summary>
    /// Gets or sets a value of Facebook page URL of the site
    /// </summary>
    public string FacebookLink { get; set; }

    /// <summary>
    /// Gets or sets a value of Twitter page URL of the site
    /// </summary>
    public string TwitterLink { get; set; }

    /// <summary>
    /// Gets or sets a value of YouTube channel URL of the site
    /// </summary>
    public string YoutubeLink { get; set; }

    /// <summary>
    /// Gets or sets a value of Instagram account URL of the site
    /// </summary>
    public string InstagramLink { get; set; }
}
