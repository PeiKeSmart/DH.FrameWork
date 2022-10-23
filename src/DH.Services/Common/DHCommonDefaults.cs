using DH.Core.Caching;

namespace DH.Services.Common
{
    /// <summary>
    /// 表示与公共服务相关的默认值
    /// </summary>
    public static partial class DHCommonDefaults
    {
        /// <summary>
        /// 获取保持活动URL的请求路径
        /// </summary>
        public static string KeepAlivePath => "keepalive/index";

        #region Address attributes

        /// <summary>
        /// 获取自定义地址属性控件的名称
        /// </summary>
        /// <remarks>
        /// {0}:地址属性id
        /// </remarks>
        public static string AddressAttributeControlName => "address_attribute_{0}";

        #endregion

        #region Maintenance

        /// <summary>
        /// 获取重新启动应用程序之前的默认超时（毫秒）
        /// </summary>
        public static int RestartTimeout => 3000;

        /// <summary>
        /// 获取数据库备份文件的路径
        /// </summary>
        public static string DbBackupsPath => "db_backups\\";

        /// <summary>
        /// 获取数据库备份文件扩展名
        /// </summary>
        public static string DbBackupFileExtension => "bak";

        #endregion

        #region Favicon and app icons

        /// <summary>
        /// 获取带有head元素代码的文件名
        /// </summary>
        public static string HeadCodeFileName => "html_code.html";

        /// <summary>
        ///  获取favicon的头部链接
        /// </summary>
        public static string SingleFaviconHeadLink => "<link rel=\"shortcut icon\" href=\"/icons/icons_{0}/{1}\">";

        /// <summary>
        /// 获取常用图标和应用图标的路径
        /// </summary>
        public static string FaviconAndAppIconsPath => "icons/icons_{0}";

        /// <summary>
        /// 获取当前商店的旧图标的名称
        /// </summary>
        public static string OldFaviconIconName => "favicon-{0}.ico";

        #endregion

        #region Localization client-side validation

        /// <summary>
        /// 获取本地化客户端验证的路径
        /// </summary>
        public static string LocalePatternPath => "lib_npm/cldr-data/main/{0}";

        /// <summary>
        /// 获取具有模板本地化功能的存档的名称
        /// </summary>
        public static string LocalePatternArchiveName => "main.zip";

        /// <summary>
        /// 获取默认模式区域设置的名称
        /// </summary>
        public static string DefaultLocalePattern => "en";

        /// <summary>
        /// 获取默认CultureInfo
        /// </summary>
        public static string DefaultLanguageCulture => "en-US";

        /// <summary>
        /// 获取要下载和安装的语言包翻译的最小进度
        /// </summary>
        public static int LanguagePackMinTranslationProgressToInstall => 80;

        /// <summary>
        /// 获取用于存储“LanguagePackProgress”值的泛型属性的名称
        /// </summary>
        public static string LanguagePackProgressAttribute => "LanguagePackProgress";

        #endregion

        #region official site

        /// <summary>
        /// 获取请求官方网站版权警告的路径
        /// </summary>
        /// <remarks>
        /// {0} : 系统URL
        /// {1} : 系统是否基于本地主机
        /// {2} : 语言代码
        /// </remarks>
        public static string DHCopyrightWarningPath => "site-warnings?url={0}&local={1}&language={2}";

        /// <summary>
        /// 获取请求官方网站获取新闻RSS的路径
        /// </summary>
        /// <remarks>
        /// {0} : 版本
        /// {1} : 系统是否基于本地主机
        /// {2} : 广告是否隐藏
        /// {3} : 系统URL
        /// {4} : 语言代码
        /// </remarks>
        public static string DHNewsRssPath => "news-rss?version={0}&localhost={1}&hideAdvertisements={2}&storeUrl={3}&language={4}";

        /// <summary>
        /// 获取请求官方站点通知成功安装的路径
        /// </summary>
        /// <remarks>
        /// {0} : 版本
        /// {1} : 系统是否基于本地主机
        /// {2} : 管理员电子邮件
        /// {3} : 系统URL
        /// {4} : 语言代码
        /// {5} : 区域性名称
        /// </remarks>
        public static string DHInstallationCompletedPath => "installation-completed?version={0}&local={1}&email={2}&url={3}&language={4}&culture={5}";

        /// <summary>
        /// 获取向官方网站请求可用类别的市场扩展的路径
        /// </summary>
        /// <remarks>
        /// {0} : 语言代码
        /// </remarks>
        public static string DHExtensionsCategoriesPath => "extensions-feed?getCategories=1&language={0}";

        /// <summary>
        /// 获取向官方网站请求可用版本的市场扩展的路径
        /// </summary>
        /// <remarks>
        /// {0} : 语言代码
        /// </remarks>
        public static string DHExtensionsVersionsPath => "extensions-feed?getVersions=1&language={0}";

        /// <summary>
        /// 获取请求官方网站进行市场扩展的路径
        /// </summary>
        /// <remarks>
        /// {0} : 扩展类别标识符
        /// {1} : 扩展版本标识符
        /// {2} : 扩展价格标识符
        /// {3} : 搜索项
        /// {4} : 页面索引
        /// {5} : 页面大小
        /// {6} : 语言代码
        /// </remarks>
        public static string DHExtensionsPath => "extensions-feed?category={0}&version={1}&price={2}&searchTerm={3}&pageIndex={4}&pageSize={5}&language={6}";

        #endregion

        #region Caching defaults

        #region Address attributes

        /// <summary>
        /// 获取用于缓存的键
        /// </summary>
        /// <remarks>
        /// {0} : 地址属性ID
        /// </remarks>
        public static CacheKey AddressAttributeValuesByAttributeCacheKey => new("DH.addressattributevalue.byattribute.{0}");

        #endregion

        #region Generic attributes

        /// <summary>
        /// 获取用于缓存的键
        /// </summary>
        /// <remarks>
        /// {0} : 实体ID
        /// {1} : 键组
        /// </remarks>
        public static CacheKey GenericAttributeCacheKey => new("DH.genericattribute.{0}-{1}");

        #endregion

        #endregion
    }
}
