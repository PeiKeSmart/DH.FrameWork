namespace DH.Core.Configuration
{
    /// <summary>
    /// 表示通用配置参数
    /// </summary>
    public partial class CommonConfig : IConfig
    {
        /// <summary>
        /// 获取或设置一个值，该值指示是否在生产环境中显示完整错误。它在开发环境中被忽略（始终启用）
        /// </summary>
        public bool DisplayFullErrorStack { get; private set; } = false;

        /// <summary>
        /// 获取或设置具有用户代理字符串的数据库路径
        /// </summary>
        public string UserAgentStringsPath { get; private set; } = "~/App_Data/browscap.xml";

        /// <summary>
        /// 获取或设置仅具有爬虫程序用户代理字符串的文件路径
        /// </summary>
        public string CrawlerOnlyUserAgentStringsPath { get; private set; } = "~/App_Data/browscap.crawlersonly.xml";

        /// <summary>
        /// 获取或设置一个值，该值指示是否在会话状态下存储TempData。默认情况下，基于cookie的TempData提供程序用于将TempData存储在cookie中。
        /// </summary>
        public bool UseSessionStateTempDataProvider { get; private set; } = false;

        /// <summary>
        /// 获取或设置一个值，该值指示是否使用MiniProfiler服务
        /// </summary>
        public bool MiniProfilerEnabled { get; private set; } = false;

        /// <summary>
        /// 运行计划任务超时前的时间长度（毫秒）。将null设置为使用默认值
        /// </summary>
        public int? ScheduleTaskRunTimeout { get; private set; } = null;

        /// <summary>
        /// 获取或设置静态内容的“缓存控制”标头值的值（秒）
        /// </summary>
        public string StaticFilesCacheControl { get; private set; } = "public,max-age=31536000";

        /// <summary>
        /// 获取或设置插件目录的静态文件扩展名黑名单
        /// </summary>
        public string PluginStaticFileExtensionsBlacklist { get; private set; } = "";

        /// <summary>
        /// 获取或设置一个值，该值指示是否为不具有可识别内容类型的文件提供服务
        /// </summary>
        /// <value></value>
        public bool ServeUnknownFileTypes { get; private set; } = false;
    }
}
