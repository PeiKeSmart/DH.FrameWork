namespace DH.Core.Configuration
{
    /// <summary>
    /// 表示与配置服务相关的默认值
    /// </summary>
    public static partial class DHConfigurationDefaults
    {
        /// <summary>
        /// 获取包含应用程序设置的文件的路径
        /// </summary>
        public static string AppSettingsFilePath => "App_Data/appsettings.json";

        /// <summary>
        /// 获取包含特定宿主环境的应用程序设置的文件的路径
        /// </summary>
        /// <remarks>0-环境名称</remarks>
        public static string AppSettingsEnvironmentFilePath => "App_Data/appsettings.{0}.json";
    }
}
