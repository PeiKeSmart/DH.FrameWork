namespace DH.Services.Media.RoxyFileman
{
    /// <summary>
    /// 表示与roxyFileman相关的默认值
    /// </summary>
    public static partial class DHRoxyFilemanDefaults
    {
        /// <summary>
        /// 上载文件的根目录的默认路径（如果未指定适当的设置）
        /// </summary>
        public static string DefaultRootDirectory { get; } = "/images/uploaded";

        /// <summary>
        /// 配置文件的路径
        /// </summary>
        public static string ConfigurationFile { get; } = "/lib/Roxy_Fileman/conf.json";

        /// <summary>
        /// 语言文件目录的路径
        /// </summary>
        public static string LanguageDirectory { get; } = "/lib/Roxy_Fileman/lang";
    }
}
