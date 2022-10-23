using DH.Core.Configuration;

namespace DH.Core.Domain.Localization
{
    /// <summary>
    /// 本地化设置
    /// </summary>
    public partial class LocalizationSettings : ISettings
    {
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
        public bool SeoFriendlyUrlsForLanguagesEnabled { get; set; }

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
}
