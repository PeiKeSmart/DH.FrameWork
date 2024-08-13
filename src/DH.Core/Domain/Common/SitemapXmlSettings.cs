using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Core.Domain.Common;

/// <summary>表示sitemap.xml设置</summary>
[DisplayName("表示sitemap.xml设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("SitemapXmlSettings")]
public class SitemapXmlSettings : Config<SitemapXmlSettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static SitemapXmlSettings() => Provider = new DbConfigProvider { UserId = 0, Category = "SitemapXml" };
    #endregion

    /// <summary>
    /// 获取或设置一个值，该值指示sitemap.xml已启用
    /// </summary>
    public bool SitemapXmlEnabled { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否将日志包含到sitemap.xml
    /// </summary>
    public bool SitemapXmlIncludeBlogPosts { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否将类别包括到sitemap.xml
    /// </summary>
    public bool SitemapXmlIncludeCategories { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否将自定义URL包含到sitemap.xml
    /// </summary>
    public bool SitemapXmlIncludeCustomUrls { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否将制造商包括到sitemap.xml
    /// </summary>
    public bool SitemapXmlIncludeManufacturers { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否将新闻包含到sitemap.xml
    /// </summary>
    public bool SitemapXmlIncludeNews { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否将产品包含到sitemap.xml
    /// </summary>
    public bool SitemapXmlIncludeProducts { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否将产品标记包含到sitemap.xml
    /// </summary>
    public bool SitemapXmlIncludeProductTags { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否将主题包含到sitemap.xml
    /// </summary>
    public bool SitemapXmlIncludeTopics { get; set; }

    /// <summary>
    /// 要添加到sitemap.xml的自定义URL列表（仅包括页面名称）
    /// </summary>
    public List<string> SitemapCustomUrls { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示网站地图文件将在多长时间后重建（以小时为单位）
    /// </summary>
    public int RebuildSitemapXmlAfterHours { get; set; }

    /// <summary>
    /// 获取或设置再次启动操作之前的等待时间（秒）
    /// </summary>
    public int SitemapBuildOperationDelay { get; set; }
}
