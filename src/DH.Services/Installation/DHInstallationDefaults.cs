namespace DH.Services.Installation
{
    /// <summary>
    /// 表示与安装服务相关的默认值
    /// </summary>
    public static partial class DHInstallationDefaults
    {
        /// <summary>
        /// 获取安装URL的请求路径
        /// </summary>
        public static string InstallPath => "install";

        /// <summary>
        /// 获取本地化资源文件的路径
        /// </summary>
        public static string LocalizationResourcesPath => "~/App_Data/Localization/";

        /// <summary>
        /// 获取本地化资源文件扩展名
        /// </summary>
        public static string LocalizationResourcesFileExtension => "dhres.xml";

        /// <summary>
        /// 获取安装示例映像的路径
        /// </summary>
        public static string SampleImagesPath => "images\\samples\\";
    }
}
