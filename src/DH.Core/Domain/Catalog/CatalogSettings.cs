using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Core.Domain.Catalog;

/// <summary>目录设置</summary>
[DisplayName("目录设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("CatalogSettings")]
public class CatalogSettings : Config<CatalogSettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static CatalogSettings() => Provider = new DbConfigProvider { UserId = 0, Category = "Catalog" };
    #endregion

    /// <summary>
    /// 获取或设置一个值，该值指示是否忽略ACL规则（侧面）。启用后，它可以显著提高性能。
    /// </summary>
    public bool IgnoreAcl { get; set; }


    /// <summary>
    /// 获取或设置一个值，该值指示是否在目录页上显示所有图片
    /// </summary>
    public bool DisplayAllPicturesOnCatalogPages { get; set; }

}
