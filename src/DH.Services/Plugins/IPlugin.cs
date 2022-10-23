namespace DH.Services.Plugins
{
    /// <summary>
    /// 接口，表示在整个编辑界面中显示的插件属性。
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 获取配置页URL
        /// </summary>
        string GetConfigurationPageUrl();

        /// <summary>
        /// 获取或设置插件描述符
        /// </summary>
        PluginDescriptor PluginDescriptor { get; set; }

        /// <summary>
        /// 安装插件
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task InstallAsync();

        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task UninstallAsync();

        /// <summary>
        /// 更新插件
        /// </summary>
        /// <param name="currentVersion">插件的当前版本</param>
        /// <param name="targetVersion">插件的新版本</param>
        /// <returns>表示异步操作的任务</returns>
        Task UpdateAsync(string currentVersion, string targetVersion);

        /// <summary>
        /// 准备卸载插件
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task PreparePluginToUninstallAsync();
    }
}
