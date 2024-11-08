﻿using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Core.Domain.Localization;

/// <summary>本地化设置</summary>
[DisplayName("本地化设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("LocalizationSettings")]
public class LocalizationSettings : Config<LocalizationSettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static LocalizationSettings() => Provider = new DbConfigProvider { UserId = 0, Category = "Localization" };
    #endregion

    /// <summary>是否启用</summary>
    public Boolean IsEnable { get; set; } = true;

    /// <summary>默认语言翻译</summary>
    public Int32 DefaultCountry { get; set; } = 1;

    /// <summary>Url没有语言SEO代码时是否跳转至当前缓存语言</summary>
    public Boolean DefaultJumpFriendlyUrls { get; set; } = false;

    /// <summary>
    /// 默认管理区域语言标识符
    /// </summary>
    public int DefaultAdminLanguageId { get; set; }

    /// <summary>
    /// 使用图像选择语言
    /// </summary>
    public bool UseImagesForLanguageSelection { get; set; }

    /// <summary>
    /// 一个值，指示是否启用具有多种语言的SEO友好URL
    /// </summary>
    public bool SeoFriendlyUrlsForLanguagesEnabled { get; set; } = true;

    /// <summary>
    /// 指示是否应按客户区域检测当前语言的值（浏览器设置）
    /// </summary>
    public bool AutomaticallyDetectLanguage { get; set; }

    /// <summary>
    /// 一个值，指示是否在应用程序启动时加载所有LocaleStringResource记录
    /// </summary>
    public bool LoadAllLocaleRecordsOnStartup { get; set; }

    /// <summary>
    /// 一个值，指示是否在应用程序启动时加载所有LocalizedProperty记录
    /// </summary>
    public bool LoadAllLocalizedPropertiesOnStartup { get; set; }

    /// <summary>
    /// 一个值，指示是否在应用程序启动时加载所有搜索引擎友好名称（slug）
    /// </summary>
    public bool LoadAllUrlRecordsOnStartup { get; set; }

    /// <summary>
    /// 一个值，指示我们是否应该忽略管理区域的RTL语言属性。
    /// 这对于使用RTL语言的店主来说很有用，但对于管理区域来说更喜欢LTR
    /// </summary>
    public bool IgnoreRtlPropertyForAdminArea { get; set; }
}
