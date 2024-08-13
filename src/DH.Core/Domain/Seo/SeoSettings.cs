using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Core.Domain.Seo;

/// <summary>SEO设置</summary>
[DisplayName("SEO设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("SeoSettings")]
public class SeoSettings : Config<SeoSettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static SeoSettings() => Provider = new DbConfigProvider { UserId = 0, Category = "Seo" };
    #endregion

    /// <summary>
    /// 页面标题分隔符
    /// </summary>
    public string PageTitleSeparator { get; set; }

    /// <summary>
    /// 页面标题SEO调整
    /// </summary>
    public PageTitleSeoAdjustment PageTitleSeoAdjustment { get; set; }

    /// <summary>
    /// 主页标题
    /// </summary>
    public string HomepageTitle { get; set; }

    /// <summary>
    /// 主页描述
    /// </summary>
    public string HomepageDescription { get; set; }

    /// <summary>
    /// 默认标题
    /// </summary>
    public string DefaultTitle { get; set; }

    /// <summary>
    /// 默认META关键字
    /// </summary>
    public string DefaultMetaKeywords { get; set; }

    /// <summary>
    /// 默认META描述
    /// </summary>
    public string DefaultMetaDescription { get; set; }

    /// <summary>
    /// 指示是否自动生成产品META描述的值（如果未输入）
    /// </summary>
    public bool GenerateProductMetaDescription { get; set; }

    /// <summary>
    /// 指示是否应将非西方字符转换为西方字符的值
    /// </summary>
    public bool ConvertNonWesternChars { get; set; }

    /// <summary>
    /// 指示是否允许unicode字符的值
    /// </summary>
    public bool AllowUnicodeCharsInUrls { get; set; }

    /// <summary>
    /// 指示是否应使用规范URL标记的值
    /// </summary>
    public bool CanonicalUrlsEnabled { get; set; }

    /// <summary>
    /// 一个值，指示是否将规范URL与查询字符串参数一起使用
    /// </summary>
    public bool QueryStringInCanonicalUrlsEnabled { get; set; }

    /// <summary>
    /// WWW要求（有或没有WWW）
    /// </summary>
    public WwwRequirement WwwRequirement { get; set; }

    /// <summary>
    /// 指示是否应生成Twitter META标记的值
    /// </summary>
    public bool TwitterMetaTags { get; set; }

    /// <summary>
    /// 指示是否应生成Open Graph META标记的值
    /// </summary>
    public bool OpenGraphMetaTags { get; set; }

    /// <summary>
    /// 保留（seName）为其他需要保留
    /// </summary>
    public List<string> ReservedUrlRecordSlugs { get; set; }

    /// <summary>
    /// <![CDATA[<head></head>]]>自定义标记部分
    /// </summary>
    public string CustomHeadTags { get; set; }

    /// <summary>
    /// 指示是否应生成Microdata标记的值
    /// </summary>
    public bool MicrodataEnabled { get; set; }
}
