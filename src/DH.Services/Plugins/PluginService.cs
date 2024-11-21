using DH.Core;
using DH.Core.Domain.Catalog;
using DH.Core.Domain.Media;
using DH.Core.Infrastructure;
using DH.Services.Customers;
using DH.Services.Localization;

using Microsoft.AspNetCore.Http;

using NewLife.Log;
using NewLife.Model;

using Pek.Exceptions;
using Pek.Infrastructure;

using XCode.Membership;

namespace DH.Services.Plugins;

/// <summary>
/// 表示插件服务实现
/// </summary>
public partial class PluginService : IPluginService
{
    private readonly ICustomerService _customerService;
    private readonly IDHFileProvider _fileProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWebHelper _webHelper;

    private readonly IPluginsInfo _pluginsInfo;

    public PluginService(
        IDHFileProvider fileProvider,
        IHttpContextAccessor httpContextAccessor,
        IWebHelper webHelper,
        ICustomerService customerService)
    {
        _pluginsInfo = ObjectContainer.Provider.GetPekService<IPluginsInfo>();
        _customerService = customerService;
        _fileProvider = fileProvider;
        _httpContextAccessor = httpContextAccessor;
        _webHelper = webHelper;
    }

    #region Utilities

    /// <summary>
    /// 检查是否基于传递的加载模式加载插件
    /// </summary>
    /// <param name="pluginDescriptor">要检查的插件描述符</param>
    /// <param name="loadMode">加载插件模式</param>
    /// <returns>检查结果</returns>
    protected virtual bool FilterByLoadMode(PluginDescriptor pluginDescriptor, LoadPluginsMode loadMode)
    {
        if (pluginDescriptor == null)
            throw new ArgumentNullException(nameof(pluginDescriptor));

        return loadMode switch
        {
            LoadPluginsMode.All => true,
            LoadPluginsMode.InstalledOnly => pluginDescriptor.Installed,
            LoadPluginsMode.NotInstalledOnly => !pluginDescriptor.Installed,
            _ => throw new NotSupportedException(nameof(loadMode)),
        };
    }

    /// <summary>
    /// 检查是否基于传递的插件组加载插件
    /// </summary>
    /// <param name="pluginDescriptor">要检查的插件描述符</param>
    /// <param name="group">组名称</param>
    /// <returns>检查结果</returns>
    protected virtual bool FilterByPluginGroup(PluginDescriptor pluginDescriptor, string group)
    {
        if (pluginDescriptor == null)
            throw new ArgumentNullException(nameof(pluginDescriptor));

        if (string.IsNullOrEmpty(group))
            return true;

        return group.Equals(pluginDescriptor.Group, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// 检查是否根据传递的客户加载插件
    /// </summary>
    /// <param name="pluginDescriptor">要检查的插件描述符</param>
    /// <param name="customer">用户</param>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含检查结果
    /// </returns>
    protected virtual bool FilterByCustomer(PluginDescriptor pluginDescriptor, User customer)
    {
        if (pluginDescriptor == null)
            throw new ArgumentNullException(nameof(pluginDescriptor));

        if (customer == null || !pluginDescriptor.LimitedToCustomerRoles.Any())
            return true;

        if (CatalogSettings.Current.IgnoreAcl)
            return true;

        return pluginDescriptor.LimitedToCustomerRoles.Intersect(_customerService.GetCustomerRoleIds(customer)).Any();
    }

    /// <summary>
    /// Check whether to load the plugin based on the store identifier passed
    /// </summary>
    /// <param name="pluginDescriptor">Plugin descriptor to check</param>
    /// <param name="storeId">Store identifier</param>
    /// <returns>Result of check</returns>
    protected virtual bool FilterByStore(PluginDescriptor pluginDescriptor, int storeId)
    {
        if (pluginDescriptor == null)
            throw new ArgumentNullException(nameof(pluginDescriptor));

        //no validation required
        if (storeId == 0)
            return true;

        if (!pluginDescriptor.LimitedToStores.Any())
            return true;

        return pluginDescriptor.LimitedToStores.Contains(storeId);
    }

    /// <summary>
    /// 检查是否基于其他插件的依赖性加载插件
    /// </summary>
    /// <param name="pluginDescriptor">要检查的插件描述符</param>
    /// <param name="dependsOnSystemName">其他插件系统名称</param>
    /// <returns>Result of check</returns>
    protected virtual bool FilterByDependsOn(PluginDescriptor pluginDescriptor, string dependsOnSystemName)
    {
        if (pluginDescriptor == null)
            throw new ArgumentNullException(nameof(pluginDescriptor));

        if (string.IsNullOrEmpty(dependsOnSystemName))
            return true;

        return pluginDescriptor.DependsOn?.Contains(dependsOnSystemName) ?? false;
    }

    /// <summary>
    /// 检查是否基于传递的插件友好名称加载插件
    /// </summary>
    /// <param name="pluginDescriptor">要检查的插件描述符</param>
    /// <param name="friendlyName">插件友好名称</param>
    /// <returns>检查结果</returns>
    protected virtual bool FilterByPluginFriendlyName(PluginDescriptor pluginDescriptor, string friendlyName)
    {
        if (pluginDescriptor == null)
            throw new ArgumentNullException(nameof(pluginDescriptor));

        if (string.IsNullOrEmpty(friendlyName))
            return true;

        return pluginDescriptor.FriendlyName.Contains(friendlyName, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// 检查是否基于传递的插件作者加载插件
    /// </summary>
    /// <param name="pluginDescriptor">要检查的插件描述符</param>
    /// <param name="author">插件作者</param>
    /// <returns>检查结果</returns>
    protected virtual bool FilterByPluginAuthor(PluginDescriptor pluginDescriptor, string author)
    {
        if (pluginDescriptor == null)
            throw new ArgumentNullException(nameof(pluginDescriptor));

        if (string.IsNullOrEmpty(author))
            return true;

        return pluginDescriptor.Author.Contains(author, StringComparison.InvariantCultureIgnoreCase);
    }

    protected virtual bool PluginsUploaded()
    {
        var pluginsDirectories =
        _fileProvider.GetDirectories(_fileProvider.MapPath(DHPluginDefaults.UploadedPath));

        if (!pluginsDirectories.Any())
            return false;
        return pluginsDirectories.Any(d =>
            _fileProvider.GetFiles(d, "*.dll").Any() || _fileProvider.GetFiles(d, "plugin.json").Any());
    }

    #endregion

    /// <summary>
    /// 清除已安装插件列表
    /// </summary>
    public virtual void ClearInstalledPluginsList()
    {
        _pluginsInfo.InstalledPlugins.Clear();
    }

    /// <summary>
    /// 删除插件
    /// </summary>
    /// <returns>表示异步操作的任务</returns>
    public virtual async Task DeletePluginsAsync()
    {
        // 获取所有卸载的插件（仅删除以前卸载的插件）
        var pluginDescriptors = _pluginsInfo.PluginDescriptors.Where(descriptor => !descriptor.pluginDescriptor.Installed).ToList();

        // 过滤器插件需要删除
        pluginDescriptors = pluginDescriptors
            .Where(descriptor => _pluginsInfo.PluginNamesToDelete.Contains(descriptor.pluginDescriptor.SystemName)).ToList();
        if (!pluginDescriptors.Any())
            return;

        // 不要通过构造函数注入服务，因为它会导致循环引用
        var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
        //var customerActivityService = EngineContext.Current.Resolve<ICustomerActivityService>();

        // 删除插件
        foreach (var descriptor in pluginDescriptors)
        {
            try
            {
                // 尝试从磁盘存储中删除插件目录
                var pluginDirectory = _fileProvider.GetDirectoryName(descriptor.pluginDescriptor.OriginalAssemblyFile);
                if (_fileProvider.DirectoryExists(pluginDirectory))
                    _fileProvider.DeleteDirectory(pluginDirectory);

                // 从适当的列表中删除插件系统名称
                _pluginsInfo.PluginNamesToDelete.Remove(descriptor.pluginDescriptor.SystemName);

                //// 活动日志
                //await customerActivityService.InsertActivityAsync("DeletePlugin",
                //    string.Format(await localizationService.GetResourceAsync("ActivityLog.DeletePlugin"), descriptor.pluginDescriptor.SystemName));
            }
            catch (Exception exception)
            {
                // 日志错误
                var message = string.Format(localizationService.GetResource("Admin.Plugins.Errors.NotDeleted"), descriptor.pluginDescriptor.SystemName);

                var webHelper = EngineContext.Current.Resolve<IWebHelper>();

                // 获取当前客户
                var currentCustomer = EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer;

                // 错误日志
                XTrace.WriteException(exception);
                LogProvider.Provider?.WriteLog("插件", "错误", false, message + " " + Environment.NewLine + exception.GetMessage(), currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
            }
        }

        //save changes
        await _pluginsInfo.SaveAsync();
    }

    /// <summary>
    /// 按类型查找与插件位于同一程序集中的插件
    /// </summary>
    /// <param name="typeInAssembly">类型</param>
    /// <returns>插件</returns>
    public virtual IPlugin FindPluginByTypeInAssembly(Type typeInAssembly)
    {
        if (typeInAssembly == null)
            throw new ArgumentNullException(nameof(typeInAssembly));

        // 试着变魔术
        var pluginDescriptor = _pluginsInfo.PluginDescriptors.FirstOrDefault(descriptor =>
            descriptor.pluginDescriptor?.ReferencedAssembly?.FullName?.Equals(typeInAssembly.Assembly.FullName,
                StringComparison.InvariantCultureIgnoreCase) ?? false);

        return pluginDescriptor.pluginDescriptor?.Instance<IPlugin>();
    }

    /// <summary>
    /// 获取所有程序集加载的集合
    /// </summary>
    /// <returns>插件加载的程序集信息列表</returns>
    public virtual IList<PluginLoadedAssemblyInfo> GetAssemblyCollisions()
    {
        return _pluginsInfo.AssemblyLoadedCollision;
    }

    /// <summary>
    /// 获取不兼容插件的名称
    /// </summary>
    /// <returns>插件名称列表</returns>
    public virtual IList<string> GetIncompatiblePlugins()
    {
        return _pluginsInfo.IncompatiblePlugins;
    }

    /// <summary>
    /// 获取插件徽标URL
    /// </summary>
    /// <param name="pluginDescriptor">插件描述符</param>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含徽标URL
    /// </returns>
    public virtual Task<string> GetPluginLogoUrlAsync(PluginDescriptor pluginDescriptor)
    {
        var pluginDirectory = _fileProvider.GetDirectoryName(pluginDescriptor.OriginalAssemblyFile);
        if (string.IsNullOrEmpty(pluginDirectory))
            return Task.FromResult<string>(null);

        // 检查支持的扩展
        var logoExtension = DHPluginDefaults.SupportedLogoImageExtensions
            .FirstOrDefault(ext => _fileProvider.FileExists(_fileProvider.Combine(pluginDirectory, $"{DHPluginDefaults.LogoFileName}.{ext}")));
        if (string.IsNullOrWhiteSpace(logoExtension))
            return Task.FromResult<string>(null);

        var pathBase = _httpContextAccessor.HttpContext.Request.PathBase.Value ?? string.Empty;
        var logoPathUrl = MediaSettings.Current.UseAbsoluteImagePath ? _webHelper.GetStoreLocation() : $"{pathBase}/";

        var logoUrl = $"{logoPathUrl}{DHPluginDefaults.PathName}/" +
            $"{_fileProvider.GetDirectoryNameOnly(pluginDirectory)}/{DHPluginDefaults.LogoFileName}.{logoExtension}";

        return Task.FromResult(logoUrl);
    }

    /// <summary>
    /// 安装插件
    /// </summary>
    /// <returns>表示异步操作的任务</returns>
    public virtual async Task InstallPluginsAsync()
    {
        // 获取所有已卸载的插件
        var pluginDescriptors = _pluginsInfo.PluginDescriptors.Where(descriptor => !descriptor.pluginDescriptor.Installed).ToList();

        // 需要安装过滤器插件
        pluginDescriptors = pluginDescriptors.Where(descriptor => _pluginsInfo.PluginNamesToInstall
            .Any(item => item.SystemName.Equals(descriptor.pluginDescriptor.SystemName))).ToList();
        if (!pluginDescriptors.Any())
            return;

        // 不要通过构造函数注入服务，因为它会导致循环引用
        var localizationService = EngineContext.Current.Resolve<ILocalizationService>();

        // 安装插件
        foreach (var descriptor in pluginDescriptors.OrderBy(pluginDescriptor => pluginDescriptor.pluginDescriptor.DisplayOrder))
        {
            try
            {
                //InsertPluginData(descriptor.pluginDescriptor.PluginType, MigrationProcessType.Installation);

                // 尝试安装实例
                await descriptor.pluginDescriptor.Instance<IPlugin>().InstallAsync();

                // 移除插件系统名称并将其添加到适当的列表中
                var pluginToInstall = _pluginsInfo.PluginNamesToInstall
                    .FirstOrDefault(plugin => plugin.SystemName.Equals(descriptor.pluginDescriptor.SystemName));
                _pluginsInfo.InstalledPlugins.Add(descriptor.pluginDescriptor.GetBaseInfoCopy);
                _pluginsInfo.PluginNamesToInstall.Remove(pluginToInstall);

                //// 活动日志
                //var customer = await _customerService.GetCustomerByGuidAsync(pluginToInstall.CustomerGuid ?? Guid.Empty);
                //await customerActivityService.InsertActivityAsync(customer, "InstallNewPlugin",
                //    string.Format(localizationService.GetResource("ActivityLog.InstallNewPlugin"), descriptor.pluginDescriptor.SystemName));

                // 将插件标记为已安装
                descriptor.pluginDescriptor.Installed = true;
                descriptor.pluginDescriptor.ShowInPluginsList = true;
            }
            catch (Exception exception)
            {
                // 将插件标记为已安装
                var message = string.Format(localizationService.GetResource("Admin.Plugins.Errors.NotInstalled"), descriptor.pluginDescriptor.SystemName);

                var webHelper = EngineContext.Current.Resolve<IWebHelper>();

                // 获取当前客户
                var currentCustomer = EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer;

                // 错误日志
                XTrace.WriteException(exception);
                LogProvider.Provider?.WriteLog("插件", "错误", false, message + " " + Environment.NewLine + exception.GetMessage(), currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
            }
        }

        // 保存更改
        await _pluginsInfo.SaveAsync();
    }

    /// <summary>
    /// 检查是否需要重新启动应用程序才能将更改应用于插件
    /// </summary>
    /// <returns>Result of check</returns>
    public virtual bool IsRestartRequired()
    {
        // 如果任何列表包含项目或某些插件已上载，则返回true
        return _pluginsInfo.PluginNamesToInstall.Any()
               || _pluginsInfo.PluginNamesToUninstall.Any()
               || _pluginsInfo.PluginNamesToDelete.Any()
               || PluginsUploaded();
    }

    /// <summary>
    /// 准备移除插件
    /// </summary>
    /// <param name="systemName">插件系统名称</param>
    /// <returns>表示异步操作的任务</returns>
    public virtual async Task PreparePluginToDeleteAsync(string systemName)
    {
        // 将插件名称添加到适当的列表（如果尚未包含）并保存更改
        if (_pluginsInfo.PluginNamesToDelete.Contains(systemName))
            return;

        _pluginsInfo.PluginNamesToDelete.Add(systemName);
        await _pluginsInfo.SaveAsync();
    }

    /// <summary>
    /// 为安装准备插件
    /// </summary>
    /// <param name="systemName">插件系统名称</param>
    /// <param name="customer">用户</param>
    /// <param name="checkDependencies">指定是否检查插件依赖项</param>
    /// <returns>表示异步操作的任务</returns>
    public virtual async Task PreparePluginToInstallAsync(string systemName, User customer = null, bool checkDependencies = true)
    {
        // 将插件名称添加到适当的列表（如果尚未包含）并保存更改
        if (_pluginsInfo.PluginNamesToInstall.Any(item => item.SystemName == systemName))
            return;

        var pluginsAfterRestart = _pluginsInfo.InstalledPlugins.Select(pd => pd.SystemName).Where(installedSystemName => !_pluginsInfo.PluginNamesToUninstall.Contains(installedSystemName)).ToList();
        pluginsAfterRestart.AddRange(_pluginsInfo.PluginNamesToInstall.Select(item => item.SystemName));

        if (checkDependencies)
        {
            var descriptor = await GetPluginDescriptorBySystemNameAsync<IPlugin>(systemName, LoadPluginsMode.NotInstalledOnly);

            if (descriptor.DependsOn?.Any() ?? false)
            {
                var dependsOn = descriptor.DependsOn
                    .Where(dependsOnSystemName => !pluginsAfterRestart.Contains(dependsOnSystemName)).ToList();

                if (dependsOn.Any())
                {
                    var dependsOnSystemNames = dependsOn.Aggregate((all, current) => $"{all}, {current}");

                    // 不要通过构造函数注入服务，因为它会导致循环引用
                    var localizationService = EngineContext.Current.Resolve<ILocalizationService>();

                    var errorMessage = string.Format(localizationService.GetResource("Admin.Plugins.Errors.InstallDependsOn"), string.IsNullOrEmpty(descriptor.FriendlyName) ? descriptor.SystemName : descriptor.FriendlyName, dependsOnSystemNames);

                    throw new DHException(errorMessage);
                }
            }
        }

        _pluginsInfo.PluginNamesToInstall.Add((systemName, customer?.ID));
        await _pluginsInfo.SaveAsync();
    }

    /// <summary>
    /// 准备卸载插件
    /// </summary>
    /// <param name="systemName">插件系统名称</param>
    /// <returns>表示异步操作的任务</returns>
    public virtual async Task PreparePluginToUninstallAsync(string systemName)
    {
        // 将插件名称添加到适当的列表（如果尚未包含）并保存更改
        if (_pluginsInfo.PluginNamesToUninstall.Contains(systemName))
            return;

        var dependentPlugins = await GetPluginDescriptorsAsync<IPlugin>(dependsOnSystemName: systemName);
        var descriptor = await GetPluginDescriptorBySystemNameAsync<IPlugin>(systemName);

        if (dependentPlugins.Any())
        {
            var dependsOn = new List<string>();

            foreach (var dependentPlugin in dependentPlugins)
            {
                if (!_pluginsInfo.InstalledPlugins.Select(pd => pd.SystemName).Contains(dependentPlugin.SystemName))
                    continue;
                if (_pluginsInfo.PluginNamesToUninstall.Contains(dependentPlugin.SystemName))
                    continue;

                dependsOn.Add(string.IsNullOrEmpty(dependentPlugin.FriendlyName)
                    ? dependentPlugin.SystemName
                    : dependentPlugin.FriendlyName);
            }

            if (dependsOn.Any())
            {
                var dependsOnSystemNames = dependsOn.Aggregate((all, current) => $"{all}, {current}");

                // 不要通过构造函数注入服务，因为它会导致循环引用
                var localizationService = EngineContext.Current.Resolve<ILocalizationService>();

                var errorMessage = string.Format(localizationService.GetResource("Admin.Plugins.Errors.UninstallDependsOn"),
                    string.IsNullOrEmpty(descriptor.FriendlyName) ? descriptor.SystemName : descriptor.FriendlyName,
                    dependsOnSystemNames);

                throw new DHException(errorMessage);
            }
        }

        var plugin = descriptor?.Instance<IPlugin>();

        if (plugin != null)
            await plugin.PreparePluginToUninstallAsync();

        _pluginsInfo.PluginNamesToUninstall.Add(systemName);
        await _pluginsInfo.SaveAsync();
    }

    /// <summary>
    /// 重置更改
    /// </summary>
    public virtual void ResetChanges()
    {
        // 清除列表并保存更改
        _pluginsInfo.PluginNamesToDelete.Clear();
        _pluginsInfo.PluginNamesToInstall.Clear();
        _pluginsInfo.PluginNamesToUninstall.Clear();
        _pluginsInfo.Save();

        // 在插件列表页面上显示所有插件
        _pluginsInfo.PluginDescriptors.ToList().ForEach(pluginDescriptor => pluginDescriptor.pluginDescriptor.ShowInPluginsList = true);

        // 清除上载的目录
        foreach (var directory in _fileProvider.GetDirectories(_fileProvider.MapPath(DHPluginDefaults.UploadedPath)))
            _fileProvider.DeleteDirectory(directory);
    }

    /// <summary>
    /// 卸载插件
    /// </summary>
    /// <returns>表示异步操作的任务</returns>
    public virtual async Task UninstallPluginsAsync()
    {
        // 获取所有已安装的插件
        var pluginDescriptors = _pluginsInfo.PluginDescriptors.Where(descriptor => descriptor.pluginDescriptor.Installed).ToList();

        // 过滤器插件需要卸载
        pluginDescriptors = pluginDescriptors
            .Where(descriptor => _pluginsInfo.PluginNamesToUninstall.Contains(descriptor.pluginDescriptor.SystemName)).ToList();
        if (!pluginDescriptors.Any())
            return;

        // 不要通过构造函数注入服务，因为它会导致循环引用
        var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
        //var customerActivityService = EngineContext.Current.Resolve<ICustomerActivityService>();

        // 卸载插件
        foreach (var descriptor in pluginDescriptors.OrderByDescending(pluginDescriptor => pluginDescriptor.pluginDescriptor.DisplayOrder))
        {
            try
            {
                var plugin = descriptor.pluginDescriptor.Instance<IPlugin>();
                // 尝试卸载实例
                await plugin.UninstallAsync();

                //// 清除数据库中的插件数据
                //DeletePluginData(descriptor.pluginDescriptor.PluginType);

                // 从适当的列表中删除插件系统名称
                _pluginsInfo.InstalledPlugins.Remove(descriptor.pluginDescriptor);
                _pluginsInfo.PluginNamesToUninstall.Remove(descriptor.pluginDescriptor.SystemName);

                //// 活动日志
                //await customerActivityService.InsertActivityAsync("UninstallPlugin",
                //    string.Format(await localizationService.GetResourceAsync("ActivityLog.UninstallPlugin"), descriptor.pluginDescriptor.SystemName));

                // 将插件标记为已卸载
                descriptor.pluginDescriptor.Installed = false;
                descriptor.pluginDescriptor.ShowInPluginsList = true;
            }
            catch (Exception exception)
            {
                // 日志错误
                var message = string.Format(localizationService.GetResource("Admin.Plugins.Errors.NotUninstalled"), descriptor.pluginDescriptor.SystemName);

                var webHelper = EngineContext.Current.Resolve<IWebHelper>();

                // 获取当前客户
                var currentCustomer = EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer;

                // 错误日志
                XTrace.WriteException(exception);
                LogProvider.Provider?.WriteLog("插件", "错误", false, message + " " + Environment.NewLine + exception.GetMessage(), currentCustomer.User.ID, currentCustomer.User.Name, webHelper.GetCurrentIpAddress());
            }
        }

        // 保存更改
        await _pluginsInfo.SaveAsync();
    }

    /// <summary>
    /// 更新插件
    /// </summary>
    /// <returns>表示异步操作的任务</returns>
    public virtual async Task UpdatePluginsAsync()
    {
        foreach (var installedPlugin in _pluginsInfo.InstalledPlugins)
        {
            var newVersion = _pluginsInfo.PluginDescriptors.FirstOrDefault(pd =>
                pd.pluginDescriptor.SystemName.Equals(installedPlugin.SystemName, StringComparison.InvariantCultureIgnoreCase));

            if (newVersion.pluginDescriptor == null)
                continue;

            if (installedPlugin.Version == newVersion.pluginDescriptor.Version)
                continue;

            //// 如果存在，则从插件运行新迁移
            //InsertPluginData(newVersion.pluginDescriptor.PluginType, MigrationProcessType.Update);

            // 运行插件更新逻辑
            await newVersion.pluginDescriptor.Instance<IPlugin>().UpdateAsync(installedPlugin.Version, newVersion.pluginDescriptor.Version);

            // 更新已安装的插件信息
            installedPlugin.Version = newVersion.pluginDescriptor.Version;
        }

        await _pluginsInfo.SaveAsync();
    }

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
    public virtual async Task<PluginDescriptor> GetPluginDescriptorBySystemNameAsync<TPlugin>(string systemName,
        LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
        User customer = null, int storeId = 0, string @group = null) where TPlugin : class, IPlugin
    {
        return (await GetPluginDescriptorsAsync<TPlugin>(loadMode, customer, storeId, group))
            .FirstOrDefault(descriptor => descriptor.SystemName.Equals(systemName));
    }

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
    public virtual async Task<IList<PluginDescriptor>> GetPluginDescriptorsAsync<TPlugin>(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
        User customer = null, int storeId = 0, string group = null, string dependsOnSystemName = "", string friendlyName = null, string author = null) where TPlugin : class, IPlugin
    {
        var pluginDescriptors = _pluginsInfo.PluginDescriptors.Select(p => p.pluginDescriptor).ToList();

        // 插件过滤器
        pluginDescriptors = await pluginDescriptors.Where(descriptor =>
            FilterByLoadMode(descriptor, loadMode) &&
            FilterByCustomer(descriptor, customer) &&
            FilterByStore(descriptor, storeId) &&
            FilterByPluginGroup(descriptor, group) &&
            FilterByDependsOn(descriptor, dependsOnSystemName) &&
            FilterByPluginFriendlyName(descriptor, friendlyName) &&
            FilterByPluginAuthor(descriptor, author)).ToListAsync();

        // 按传递类型筛选
        if (typeof(TPlugin) != typeof(IPlugin))
            pluginDescriptors = pluginDescriptors.Where(descriptor => typeof(TPlugin).IsAssignableFrom(descriptor.PluginType)).ToList();

        // 按组名称排序
        pluginDescriptors = pluginDescriptors.OrderBy(descriptor => descriptor.Group)
            .ThenBy(descriptor => descriptor.DisplayOrder).ToList();

        return pluginDescriptors;
    }

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
    public virtual async Task<IList<TPlugin>> GetPluginsAsync<TPlugin>(
        LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
        User customer = null, int storeId = 0, string @group = null) where TPlugin : class, IPlugin
    {
        return (await GetPluginDescriptorsAsync<TPlugin>(loadMode, customer, storeId, group))
            .Select(descriptor => descriptor.Instance<TPlugin>()).ToList();
    }

}
