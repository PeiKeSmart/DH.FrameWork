using System.Reflection;
using System.Text;

using Newtonsoft.Json;

using Pek;
using Pek.Infrastructure;

namespace DH.Services.Plugins;

/// <summary>
/// 表示有关插件的信息
/// </summary>
public partial class PluginsInfo : IPluginsInfo
{
    #region Fields

    private const string OBSOLETE_FIELD = "Obsolete field, using only for compatibility";
    private List<string> _installedPluginNames = new();
    private IList<PluginDescriptorBaseInfo> _installedPlugins = new List<PluginDescriptorBaseInfo>();

    protected readonly IDHFileProvider _fileProvider;

    #endregion

    #region Utilities

    /// <summary>
    /// 从过时文件中获取已安装插件的系统名称
    /// </summary>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含插件系统名称列表
    /// </returns>
    protected virtual IList<string> GetObsoleteInstalledPluginNames()
    {
        // 检查文件是否存在
        var filePath = _fileProvider.MapPath(DHPluginDefaults.InstalledPluginsFilePath);
        if (!_fileProvider.FileExists(filePath))
        {
            //如果没有，请尝试解析以前的版本中使用的文件
            filePath = _fileProvider.MapPath(DHPluginDefaults.ObsoleteInstalledPluginsFilePath);
            if (!_fileProvider.FileExists(filePath))
                return new List<string>();

            // 从旧的txt文件中获取插件系统名称
            var pluginSystemNames = new List<string>();
            using (var reader = new StringReader(_fileProvider.ReadAllText(filePath, Encoding.UTF8)))
            {
                string pluginName;
                while ((pluginName = reader.ReadLine()) != null)
                    if (!string.IsNullOrWhiteSpace(pluginName))
                        pluginSystemNames.Add(pluginName.Trim());
            }

            // 并删除旧的
            _fileProvider.DeleteFile(filePath);

            return pluginSystemNames;
        }

        var text = _fileProvider.ReadAllText(filePath, Encoding.UTF8);
        if (string.IsNullOrEmpty(text))
            return new List<string>();

        // 删除旧文件
        _fileProvider.DeleteFile(filePath);

        // 从JSON文件获取插件系统名称
        return JsonConvert.DeserializeObject<IList<string>>(text);
    }

    /// <summary>
    /// 从json反序列化插件信息
    /// </summary>
    /// <param name="json">PluginInfo的Json数据</param>
    /// <returns>如果加载了数据，则为True，否则为False</returns>
    protected virtual void DeserializePluginInfo(string json)
    {
        if (string.IsNullOrEmpty(json))
            return;

        var pluginsInfo = JsonConvert.DeserializeObject<PluginsInfo>(json);

        if (pluginsInfo == null)
            return;

        InstalledPluginNames = pluginsInfo.InstalledPluginNames;
        InstalledPlugins = pluginsInfo.InstalledPlugins;
        PluginNamesToUninstall = pluginsInfo.PluginNamesToUninstall;
        PluginNamesToDelete = pluginsInfo.PluginNamesToDelete;
        PluginNamesToInstall = pluginsInfo.PluginNamesToInstall;
    }

    /// <summary>
    /// 检查目录是否为插件目录
    /// </summary>
    /// <param name="directoryName">目录名</param>
    /// <returns>检查结果</returns>
    protected bool IsPluginDirectory(string directoryName)
    {
        if (string.IsNullOrEmpty(directoryName))
            return false;

        // 获取父目录
        var parent = _fileProvider.GetParentDirectory(directoryName);
        if (string.IsNullOrEmpty(parent))
            return false;

        // 目录直接位于插件目录中
        if (!_fileProvider.GetDirectoryNameOnly(parent).Equals(DHPluginDefaults.PathName, StringComparison.InvariantCultureIgnoreCase))
            return false;

        return true;
    }

    /// <summary>
    /// 获取描述文件列表插件描述符对
    /// </summary>
    /// <param name="directoryName">插件目录名</param>
    /// <returns>原始和解析的描述文件</returns>
    protected IList<(string DescriptionFile, PluginDescriptor PluginDescriptor)> GetDescriptionFilesAndDescriptors(string directoryName)
    {
        if (string.IsNullOrEmpty(directoryName))
            throw new ArgumentNullException(nameof(directoryName));

        var result = new List<(string DescriptionFile, PluginDescriptor PluginDescriptor)>();

        // 尝试在插件目录中查找描述文件
        var files = _fileProvider.GetFiles(directoryName, DHPluginDefaults.DescriptionFileName, false);

        // 填充结果列表
        foreach (var descriptionFile in files)
        {
            // 跳过不在插件目录中的文件
            if (!IsPluginDirectory(_fileProvider.GetDirectoryName(descriptionFile)))
                continue;

            // 从文件加载插件描述符
            var text = _fileProvider.ReadAllText(descriptionFile, Encoding.UTF8);
            var pluginDescriptor = PluginDescriptor.GetPluginDescriptorFromText(text);

            result.Add((descriptionFile, pluginDescriptor));
        }

        // 按显示顺序对列表进行排序。注：最低显示顺序为第一，即0、1、1、5、10
        result = result.OrderBy(item => item.PluginDescriptor.DisplayOrder).ToList();

        return result;
    }

    #endregion

    #region 初始化

    public PluginsInfo(IDHFileProvider fileProvider)
    {
        _fileProvider = fileProvider ?? CommonHelper.DefaultFileProvider;
    }

    #endregion

    #region Methods

    /// <summary>
    /// 获取插件信息
    /// </summary>
    /// <returns>
    /// 如果加载了数据，则为true，否则为False
    /// </returns>
    public virtual void LoadPluginInfo()
    {
        // 检查插件信息文件是否存在
        var filePath = _fileProvider.MapPath(DHPluginDefaults.PluginsInfoFilePath);
        if (!_fileProvider.FileExists(filePath))
        {
            // 文件不存在，因此尝试从过时文件中仅获取已安装的插件名称
            _installedPluginNames.AddRange(GetObsoleteInstalledPluginNames());

            // 并根据需要将信息保存到新文件中
            if (_installedPluginNames.Any())
                Save();
        }

        // 尝试从JSON文件获取插件信息
        var text = _fileProvider.FileExists(filePath)
            ? _fileProvider.ReadAllText(filePath, Encoding.UTF8)
            : string.Empty;

        DeserializePluginInfo(text);

        var pluginDescriptors = new List<(PluginDescriptor pluginDescriptor, bool needToDeploy)>();
        var incompatiblePlugins = new List<string>();

        // 确保插件目录已创建
        var pluginsDirectory = _fileProvider.MapPath(DHPluginDefaults.Path);
        _fileProvider.CreateDirectory(pluginsDirectory);

        // 从插件目录加载插件描述符
        foreach (var item in GetDescriptionFilesAndDescriptors(pluginsDirectory))
        {
            var descriptionFile = item.DescriptionFile;
            var pluginDescriptor = item.PluginDescriptor;

            // 跳过要删除的插件的描述符
            if (PluginNamesToDelete.Contains(pluginDescriptor.SystemName))
                continue;

            // 确保插件与当前版本兼容
            if (!pluginDescriptor.SupportedVersions.Contains(Core.DHVersion.CURRENT_VERSION, StringComparer.InvariantCultureIgnoreCase))
            {
                incompatiblePlugins.Add(pluginDescriptor.SystemName);
                continue;
            }

            // 更多验证
            if (string.IsNullOrEmpty(pluginDescriptor.SystemName?.Trim()))
                throw new Exception($"A plugin '{descriptionFile}' has no system name. Try assigning the plugin a unique name and recompiling.");

            if (pluginDescriptors.Any(p => p.pluginDescriptor.Equals(pluginDescriptor)))
                throw new Exception($"A plugin with '{pluginDescriptor.SystemName}' system name is already defined");

            // 设置'Installed'属性
            pluginDescriptor.Installed = InstalledPlugins.Select(pd => pd.SystemName)
                .Any(pluginName => pluginName.Equals(pluginDescriptor.SystemName, StringComparison.InvariantCultureIgnoreCase));

            try
            {
                // 尝试获取插件目录
                var pluginDirectory = _fileProvider.GetDirectoryName(descriptionFile);
                if (string.IsNullOrEmpty(pluginDirectory))
                    throw new Exception($"Directory cannot be resolved for '{_fileProvider.GetFileName(descriptionFile)}' description file");

                // 获取插件目录中所有库文件的列表（不在bin目录中）
                pluginDescriptor.PluginFiles = _fileProvider.GetFiles(pluginDirectory, "*.dll", false)
                    .Where(file => IsPluginDirectory(_fileProvider.GetDirectoryName(file)))
                    .ToList();

                // 尝试查找主插件程序集文件
                var mainPluginFile = pluginDescriptor.PluginFiles.FirstOrDefault(file =>
                {
                    var fileName = _fileProvider.GetFileName(file);
                    return fileName.Equals(pluginDescriptor.AssemblyFileName, StringComparison.InvariantCultureIgnoreCase);
                });

                // 找不到具有指定名称的文件
                if (mainPluginFile == null)
                {
                    // 所以插件不兼容
                    incompatiblePlugins.Add(pluginDescriptor.SystemName);
                    continue;
                }

                var pluginName = pluginDescriptor.SystemName;

                // 如果找到，请将其设置为原始程序集文件
                pluginDescriptor.OriginalAssemblyFile = mainPluginFile;

                // 如果插件已安装，则需要部署
                var needToDeploy = InstalledPlugins.Select(pd => pd.SystemName).Contains(pluginName);

                // 此外，如果插件现在只安装，则进行部署
                needToDeploy = needToDeploy || PluginNamesToInstall.Any(pluginInfo => pluginInfo.SystemName.Equals(pluginName));

                // 最后，将要删除的插件排除在部署之外
                needToDeploy = needToDeploy && !PluginNamesToDelete.Contains(pluginName);

                // 将插件标记为已成功部署
                pluginDescriptors.Add((pluginDescriptor, needToDeploy));
            }
            catch (ReflectionTypeLoadException exception)
            {
                // 获取所有加载器异常
                var error = exception.LoaderExceptions.Aggregate($"Plugin '{pluginDescriptor.FriendlyName}'. ",
                    (message, nextMessage) => $"{message}{nextMessage?.Message ?? string.Empty}{Environment.NewLine}");

                throw new Exception(error, exception);
            }
            catch (Exception exception)
            {
                // 添加一个插件名称，这样我们就可以轻松识别有问题的插件
                throw new Exception($"Plugin '{pluginDescriptor.FriendlyName}'. {exception.Message}", exception);
            }
        }

        IncompatiblePlugins = incompatiblePlugins;
        PluginDescriptors = pluginDescriptors;
    }

    /// <summary>
    /// 将插件信息保存到文件
    /// </summary>
    /// <returns>表示异步操作的任务</returns>
    public virtual async Task SaveAsync()
    {
        // 保存文件
        var filePath = _fileProvider.MapPath(DHPluginDefaults.PluginsInfoFilePath);
        var text = JsonConvert.SerializeObject(this, Formatting.Indented);
        await _fileProvider.WriteAllTextAsync(filePath, text, Encoding.UTF8);
    }

    /// <summary>
    /// 将插件信息保存到文件
    /// </summary>
    public virtual void Save()
    {
        // 保存文件
        var filePath = _fileProvider.MapPath(DHPluginDefaults.PluginsInfoFilePath);
        var text = JsonConvert.SerializeObject(this, Formatting.Indented);
        _fileProvider.WriteAllText(filePath, text, Encoding.UTF8);
    }

    /// <summary>
    /// 从IPluginsInfo接口的另一个实例创建副本
    /// </summary>
    /// <param name="pluginsInfo">Plugins info</param>
    public virtual void CopyFrom(IPluginsInfo pluginsInfo)
    {
        InstalledPlugins = pluginsInfo.InstalledPlugins?.ToList() ?? new List<PluginDescriptorBaseInfo>();
        PluginNamesToUninstall = pluginsInfo.PluginNamesToUninstall?.ToList() ?? new List<string>();
        PluginNamesToDelete = pluginsInfo.PluginNamesToDelete?.ToList() ?? new List<string>();
        PluginNamesToInstall = pluginsInfo.PluginNamesToInstall?.ToList() ??
                               new List<(string SystemName, Int32? CustomerGuid)>();
        AssemblyLoadedCollision = pluginsInfo.AssemblyLoadedCollision?.ToList();
        PluginDescriptors = pluginsInfo.PluginDescriptors;
        IncompatiblePlugins = pluginsInfo.IncompatiblePlugins?.ToList();
    }

    #endregion

    #region Properties

    /// <summary>
    /// 获取或设置所有已安装插件名称的列表
    /// </summary>
    public virtual IList<string> InstalledPluginNames
    {
        get
        {
            if (_installedPlugins.Any())
                _installedPluginNames.Clear();

            return _installedPluginNames.Any() ? _installedPluginNames : new List<string> { OBSOLETE_FIELD };
        }
        set
        {
            if (value?.Any() ?? false)
                _installedPluginNames = value.ToList();
        }
    }

    /// <summary>
    /// 获取或设置所有已安装插件的列表
    /// </summary>
    public virtual IList<PluginDescriptorBaseInfo> InstalledPlugins
    {
        get
        {
            if ((_installedPlugins?.Any() ?? false) || !_installedPluginNames.Any())
                return _installedPlugins;

            if (PluginDescriptors?.Any() ?? false)
                _installedPlugins = PluginDescriptors
                    .Where(pd => _installedPluginNames.Any(pn =>
                        pn.Equals(pd.pluginDescriptor.SystemName, StringComparison.InvariantCultureIgnoreCase)))
                    .Select(pd => pd.pluginDescriptor as PluginDescriptorBaseInfo).ToList();
            else
                return _installedPluginNames
                    .Where(name => !name.Equals(OBSOLETE_FIELD, StringComparison.InvariantCultureIgnoreCase))
                    .Select(systemName => new PluginDescriptorBaseInfo { SystemName = systemName }).ToList();

            return _installedPlugins;
        }
        set => _installedPlugins = value;
    }

    /// <summary>
    /// 获取或设置要卸载的插件名称列表
    /// </summary>
    public virtual IList<string> PluginNamesToUninstall { get; set; } = new List<string>();

    /// <summary>
    /// 获取或设置将被删除的插件名称列表
    /// </summary>
    public virtual IList<string> PluginNamesToDelete { get; set; } = new List<string>();

    /// <summary>
    /// 获取或设置将安装的插件名称列表
    /// </summary>
    public virtual IList<(string SystemName, Int32? CustomerGuid)> PluginNamesToInstall { get; set; } =
        new List<(string SystemName, Int32? CustomerGuid)>();

    /// <summary>
    /// 获取或设置与当前版本不兼容的插件名称列表
    /// </summary>
    [JsonIgnore]
    public virtual IList<string> IncompatiblePlugins { get; set; }

    /// <summary>
    /// 获取或设置程序集加载冲突的列表
    /// </summary>
    [JsonIgnore]
    public virtual IList<PluginLoadedAssemblyInfo> AssemblyLoadedCollision { get; set; }

    /// <summary>
    /// 获取或设置所有已部署插件的插件描述符集合
    /// </summary>
    [JsonIgnore]
    public virtual IList<(PluginDescriptor pluginDescriptor, bool needToDeploy)> PluginDescriptors { get; set; }

    #endregion
}
