using DH.Core.Caching;

namespace DH.Services.Plugins
{
    /// <summary>
    /// 表示与插件相关的默认值
    /// </summary>
    public static partial class DHPluginDefaults
    {
        /// <summary>
        /// 获取包含（在早期版本中）已安装插件系统名称的文件的路径
        /// </summary>
        public static string ObsoleteInstalledPluginsFilePath => "~/App_Data/InstalledPlugins.txt";

        /// <summary>
        /// 获取包含已安装插件系统名称的文件的路径
        /// </summary>
        public static string InstalledPluginsFilePath => "~/App_Data/installedPlugins.json";

        /// <summary>
        /// 获取包含已安装插件系统名称的文件的路径
        /// </summary>
        public static string PluginsInfoFilePath => "~/App_Data/plugins.json";

        /// <summary>
        /// 获取插件文件夹的路径
        /// </summary>
        public static string Path => "~/Plugins";

        /// <summary>
        /// 获取插件文件夹的路径
        /// </summary>
        public static string UploadedPath => "~/Plugins/Uploaded";

        /// <summary>
        /// 获取插件文件夹名称
        /// </summary>
        public static string PathName => "Plugins";

        /// <summary>
        /// 获取插件refs文件夹的路径
        /// </summary>
        public static string RefsPathName => "refs";

        /// <summary>
        /// 获取插件描述文件的名称
        /// </summary>
        public static string DescriptionFileName => "plugin.json";

        /// <summary>
        /// 获取插件徽标文件名
        /// </summary>
        public static string LogoFileName => "logo";

        /// <summary>
        /// 获取徽标文件的支持扩展名
        /// </summary>
        public static List<string> SupportedLogoImageExtensions => new() { "jpg", "png", "gif" };

        /// <summary>
        /// 获取上载的临时目录的路径
        /// </summary>
        public static string UploadsTempPath => "~/App_Data/TempUploads";

        /// <summary>
        /// 获取包含有关上载项的信息的文件的名称
        /// </summary>
        public static string UploadedItemsFileName => "uploadedItems.json";

        /// <summary>
        /// 获取主题文件夹的路径
        /// </summary>
        public static string ThemesPath => "~/Themes";

        /// <summary>
        /// 获取主题描述文件的名称
        /// </summary>
        public static string ThemeDescriptionFileName => "theme.json";

        /// <summary>
        /// 获取用于缓存管理导航插件的键
        /// </summary>
        /// <remarks>
        /// {0} : customer identifier
        /// </remarks>
        public static CacheKey AdminNavigationPluginsCacheKey => new("DH.plugins.adminnavigation.{0}", AdminNavigationPluginsPrefix);

        /// <summary>
        /// 获取要清除缓存的键模式
        /// </summary>
        public static string AdminNavigationPluginsPrefix => "DH.plugins.adminnavigation.";
    }
}
