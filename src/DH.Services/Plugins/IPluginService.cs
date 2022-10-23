using XCode.Membership;

namespace DH.Services.Plugins
{
    /// <summary>
    /// 表示插件服务
    /// </summary>
    public partial interface IPluginService
    {
        /// <summary>
        /// 获取插件描述符
        /// </summary>
        /// <typeparam name="TPlugin">要获取的插件类型</typeparam>
        /// <param name="loadMode">按加载插件模式筛选</param>
        /// <param name="customer">按客户筛选；传递null以加载所有记录</param>
        /// <param name="storeId">按存储筛选；传递0以加载所有记录</param>
        /// <param name="group">按插件组过滤；传递null以加载所有记录</param>
        /// <param name="friendlyName">按插件友好名称筛选；传递null以加载所有记录</param>
        /// <param name="author">按插件作者筛选；传递null以加载所有记录</param>
        /// <param name="dependsOnSystemName">用于定义依赖项的插件的系统名称</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含插件描述符
        /// </returns>
        Task<IList<PluginDescriptor>> GetPluginDescriptorsAsync<TPlugin>(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
            User customer = null, int storeId = 0, string group = null, string dependsOnSystemName = "", string friendlyName = null, string author = null) where TPlugin : class, IPlugin;

        /// <summary>
        /// 通过系统名称获取插件描述符
        /// </summary>
        /// <typeparam name="TPlugin">要获取的插件类型</typeparam>
        /// <param name="systemName">插件系统名称</param>
        /// <param name="loadMode">加载插件模式</param>
        /// <param name="customer">按客户筛选；传递null以加载所有记录</param>
        /// <param name="storeId">按存储筛选；传递0以加载所有记录</param>
        /// <param name="group">按插件组过滤；传递null以加载所有记录</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含>插件描述符
        /// </returns>
        Task<PluginDescriptor> GetPluginDescriptorBySystemNameAsync<TPlugin>(string systemName,
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
            User customer = null, int storeId = 0, string group = null) where TPlugin : class, IPlugin;

        /// <summary>
        /// 获取插件
        /// </summary>
        /// <typeparam name="TPlugin">要获取的插件类型</typeparam>
        /// <param name="loadMode">按加载插件模式筛选</param>
        /// <param name="customer">按客户筛选；传递null以加载所有记录</param>
        /// <param name="storeId">按存储筛选；传递0以加载所有记录</param>
        /// <param name="group">按插件组过滤；传递null以加载所有记录</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含插件
        /// </returns>
        Task<IList<TPlugin>> GetPluginsAsync<TPlugin>(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
            User customer = null, int storeId = 0, string group = null) where TPlugin : class, IPlugin;

        /// <summary>
        /// 按类型查找与插件位于同一程序集中的插件
        /// </summary>
        /// <param name="typeInAssembly">类型</param>
        /// <returns>插件</returns>
        IPlugin FindPluginByTypeInAssembly(Type typeInAssembly);

        /// <summary>
        /// 获取插件徽标URL
        /// </summary>
        /// <param name="pluginDescriptor">插件描述符</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含徽标URL
        /// </returns>
        Task<string> GetPluginLogoUrlAsync(PluginDescriptor pluginDescriptor);

        /// <summary>
        /// 为安装准备插件
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        /// <param name="customer">会员</param>
        /// <param name="checkDependencies">指定是否检查插件依赖项</param>
        /// <returns>表示异步操作的任务</returns>
        Task PreparePluginToInstallAsync(string systemName, User customer = null, bool checkDependencies = true);

        /// <summary>
        /// 准备卸载插件
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        /// <returns>表示异步操作的任务</returns>
        Task PreparePluginToUninstallAsync(string systemName);

        /// <summary>
        /// 准备移除插件
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        /// <returns>表示异步操作的任务</returns>
        Task PreparePluginToDeleteAsync(string systemName);

        /// <summary>
        /// 重置更改
        /// </summary>
        void ResetChanges();

        /// <summary>
        /// 清除已安装插件列表
        /// </summary>
        void ClearInstalledPluginsList();

        /// <summary>
        /// 安装插件
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task InstallPluginsAsync();

        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task UninstallPluginsAsync();

        /// <summary>
        /// 删除插件
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task DeletePluginsAsync();

        /// <summary>
        /// 更新插件
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task UpdatePluginsAsync();

        /// <summary>
        /// 检查是否需要重新启动应用程序才能将更改应用于插件
        /// </summary>
        /// <returns>检查结果</returns>
        bool IsRestartRequired();

        /// <summary>
        /// 获取不兼容插件的名称
        /// </summary>
        /// <returns>插件名称列表</returns>
        IList<string> GetIncompatiblePlugins();

        /// <summary>
        /// 获取所有程序集加载的列表
        /// </summary>
        /// <returns>插件加载的程序集信息列表</returns>
        IList<PluginLoadedAssemblyInfo> GetAssemblyCollisions();
    }
}
