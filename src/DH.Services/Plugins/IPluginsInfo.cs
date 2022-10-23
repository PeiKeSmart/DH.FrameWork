namespace DH.Services.Plugins
{
    /// <summary>
    /// 表示有关插件的信息
    /// </summary>
    public interface IPluginsInfo
    {
        /// <summary>
        /// 将插件信息保存到文件
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task SaveAsync();

        /// <summary>
        /// 获取插件信息
        /// </summary>
        /// <returns>
        /// 如果加载了数据，则为true，否则为False
        /// </returns>
        void LoadPluginInfo();

        /// <summary>
        /// 将插件信息保存到文件
        /// </summary>
        void Save();

        /// <summary>
        /// 从IPluginsInfo接口的另一个实例创建副本
        /// </summary>
        /// <param name="pluginsInfo">插件信息</param>
        void CopyFrom(IPluginsInfo pluginsInfo);

        /// <summary>
        /// 获取或设置所有已安装插件的列表
        /// </summary>
        IList<PluginDescriptorBaseInfo> InstalledPlugins { get; set; }

        /// <summary>
        /// 获取或设置要卸载的插件名称列表
        /// </summary>
        IList<string> PluginNamesToUninstall { get; set; }

        /// <summary>
        /// 获取或设置将被删除的插件名称列表
        /// </summary>
        IList<string> PluginNamesToDelete { get; set; }

        /// <summary>
        /// 获取或设置将安装的插件名称列表
        /// </summary>
        IList<(string SystemName, Guid? CustomerGuid)> PluginNamesToInstall { get; set; }

        /// <summary>
        /// 获取或设置程序集加载冲突的列表
        /// </summary>
        IList<PluginLoadedAssemblyInfo> AssemblyLoadedCollision { get; set; }

        /// <summary>
        /// 获取或设置所有已部署插件的插件描述符集合
        /// </summary>
        IList<(PluginDescriptor pluginDescriptor, bool needToDeploy)> PluginDescriptors { get; set; }

        /// <summary>
        /// 获取或设置与当前版本不兼容的插件名称列表
        /// </summary>
        IList<string> IncompatiblePlugins { get; set; }
    }
}
